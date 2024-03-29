﻿using Control_Impressora.Context;
using Control_Impressora.Models;
using Control_Impressora.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Controllers
{
    public class AccountController : Controller
    {
        private readonly Contexto _context;

    

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, Contexto contexto)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = contexto;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

   

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var endereco = await _context.Endereco.ToListAsync();
            endereco.Insert(0, new Endereco()
            {
                Id = 0,
                Cidade = "Selecione a Cidade"
            });
            ViewBag.Endereco = endereco;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register( RegisterViewModel model)
        {
            string nomek = model.Nome;

            if (ModelState.IsValid)
            {
                // Copia os dados do RegisterViewModel para o IdentityUser
                var user = new ApplicationUser
                {
                    Nome = nomek.ToString(),
                    UserName = model.Email,
                    Email = model.Email,
                    Cpf = model.Cpf,
                    Rg = model.Rg, 
                    Cargo = model.Cargo,
                    Matricula = model.Matricula,
                    EnderecoId = model.EnderecoId
                };
                // Armazena os dados do usuário na tabela AspNetUsers
                var result = await userManager.CreateAsync(user, model.Password);
                // Se o usuário foi criado com sucesso, faz o login do usuário
                // usando o serviço SignInManager e redireciona para o Método Action Index
                if (result.Succeeded)
                {
                    // Se o usuário estiver logado e for do perfil Admin
                    // então o usuário é o usuário Admin que esta criando
                    // um novo usuário, assim vamos direcioná-lo para o
                    // usuário Admin para a Action ListRoles
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Inicio", "Impressora");
                }
                // Se houver erros então inclui no ModelState
                // que será exibido pela tag helper summary na validação
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Inicio", "Impressora");
                }
                ModelState.AddModelError(string.Empty, "Login Inválido");
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"O Email {email} já está sendo usado.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
