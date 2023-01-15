using Labote.Core.BindingModels;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core
{
    public interface ILaboteContextDbSeed
    {
        Task<bool> CreateRoleAndUsersAsync();
        Task<bool> CreateDefaultRoleAsync();
        Task<bool> MenuSeedAsync();

    }

    public class LaboteContextDbSeed : ILaboteContextDbSeed
    {
        private readonly UserManager<LaboteUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public IConfiguration Configuration { get; }

        public LaboteContextDbSeed(UserManager<LaboteUser> userManager, RoleManager<UserRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Configuration = configuration;
        }



        public async Task<bool> CreateRoleAndUsersAsync()
        {
            try
            {
                var userTopic = new UserTopic
                {
                };
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {

                        context.UserTopics.Add(userTopic);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }



                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                  
                        LaboteUser user = new LaboteUser();
                        if (_userManager.Users.Count() == 0)
                        {
                            user = new LaboteUser()
                            {
                                Email = "mail@mail.com",
                                SecurityStamp = Guid.NewGuid().ToString(),
                                UserName = "Varsayilan",
                                FirstName = "Varsayılan",
                                Lastname = "Kullanici",
                                EmailConfirmed = true,
                                NotDelete = true,
                                UserTopicId = userTopic.Id

                            };

                            var usr = _userManager.CreateAsync(user, "Tg71!nG*").Result;
                        }
                        if (_roleManager.Roles.Count() == 0)
                        {
                            var admninRol = _roleManager.CreateAsync(new UserRole
                            {
                                Name = "SuperAdmin",
                                NormalizedName = "SUPERADMİN",
                                NotDelete = true,
                                IsHidden = true,
                                UserTopicId = userTopic.Id
                            }).Result;
                            var userRol = _roleManager.CreateAsync(
                                new UserRole
                                {
                                    Name = "Admin",
                                    NormalizedName = "ADMİN",
                                    IsHidden = false,
                                    UserTopicId = userTopic.Id
                                }
                                ).Result;
                            var RoleAdd = _userManager.AddToRoleAsync(user, "SuperAdmin").Result;
                           

                        }
                        context.SaveChanges();
                        transaction.Commit();

                    };

                }

                using (LaboteContext context = new LaboteContext())
                {

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        if (context.UserMenuModules.Count() == 0)
                        {
                            var role = (from roles in context.Roles.Where(x => x.Name == "SuperAdmin")
                                        from userroles in context.UserRoles
                                        where roles.Id == userroles.RoleId
                                        select userroles).FirstOrDefault();

                            var data = context.MenuModules.ToList();


                            foreach (var item in data)
                            {
                                context.UserMenuModules.Add(new UserMenuModule
                                {
                                    UserRoleId = role.RoleId,
                                    MenuModelId = item.Id
                                });
                            }
                            context.SaveChanges();
                            transaction.Commit();
                        }
                    };
                }
            }
            catch (Exception e)
            {

                throw;
            }



            return true;
        }
        public async Task<bool> MenuSeedAsync()
        {
            var MenuList = new List<MenuModule>();


            using (LaboteContext context = new LaboteContext())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    MenuList = context.MenuModules.ToList();
                }
            }

            #region Dashboard
            var Dashboard = new MenuModule
            {
                IconName = "fa fa-chart-line",
                PageName = "Genel Inceleme",
                OrderNumber = 0,
                IsMainPage = false,
                PageUrl = "dashboard"
            };
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Dashboard.PageName);
                    if (Ky == null)
                    {
                        context.Add(Dashboard); //////////
                        context.SaveChanges();
                    }
                    else { Dashboard = Ky; }///////

                    transaction.Commit();
                }
            }

            #endregion

            #region Süper Admin

            var SuperAdmin = new MenuModule
            {
                IconName = "fas fa-user-shield",
                PageName = "Süper Admin",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "super-admin",
                IsSuperAdmin = true
            };
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == SuperAdmin.PageName);
                    if (Ky == null)
                    {
                        context.Add(SuperAdmin);
                        context.SaveChanges();
                    }
                    else { SuperAdmin = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Firma Özellik Tanımları"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Firma Özellik Tanımları",
                            PageUrl = "ozellik-tanimlari",
                            ParentId = SuperAdmin.Id,
                            OrderNumber = 3,
                            IsSuperAdmin = true
                        });
                    }


                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Firma Grupları"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Firma Grupları",
                            PageUrl = "firma-gruplari",
                            ParentId = SuperAdmin.Id,
                            OrderNumber = 1,
                            IsSuperAdmin = true
                        });
                    }


                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Firma Türleri"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Firma Türleri",
                            PageUrl = "firma-turleri",
                            ParentId = SuperAdmin.Id,
                            OrderNumber = 2,
                            IsSuperAdmin=true
                        });
                    }
                 

                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            #endregion


            #region Yönetimsel Araçlar

            var KullaniciYonetimi = new MenuModule
            {
                IconName = "fas fa-cogs",
                PageName = "Yönetimsel Araçlar",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "yonetimsel-araclar"
            };
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == KullaniciYonetimi.PageName);
                    if (Ky == null)
                    {
                        context.Add(KullaniciYonetimi);
                        context.SaveChanges();
                    }
                    else { KullaniciYonetimi = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Kullanıcı İşlemleri"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Kullanıcı İşlemleri",
                            PageUrl = "kullanici-islemleri",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 1,
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Görev Grupları"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Görev Grupları",
                            PageUrl = "gorev-grup",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 2
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Yetki İşlemleri"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Yetki İşlemleri",
                            PageUrl = "yetki-islemleri",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 3
                        });
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            #endregion

            #region Firmalarım

            var Firmalar = new MenuModule
            {
                IconName = "fas fa-store",
                PageName = "Firmalarım",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "firmalarim"
            };
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Firmalar.PageName);
                    if (Ky == null)
                    {
                        context.Add(Firmalar);
                        context.SaveChanges();
                    }
                    else { Firmalar = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Firma Listesi"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Firma Listesi",
                            PageUrl = "firma-listesi",
                            ParentId = Firmalar.Id,
                            OrderNumber = 1,
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

         using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Firma Özellikleri"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Firma Özellikleri",
                            PageUrl = "firma-ozellikleri",
                            ParentId = Firmalar.Id,
                            OrderNumber = 1,
                            IsHidden=true
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            #endregion


            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    var module = context.MenuModules.Where(x => x.IsMainPage == false && x.ParentId != null && !x.IsHidden).ToList();

                    foreach (var item in module)
                    {
                        var isExist = context.MenuModules.Where(x => x.ParentId == item.Id && x.IsHidden).Any();

                        if (!isExist)
                        {
                            context.MenuModules.Add(new MenuModule
                            {
                                IconName = "icon-pencil",
                                ParentId = item.Id,
                                PageName = "Yazma",
                                IsHidden = true,
                                PageUrl = item.PageUrl

                            });
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            return true;
        }

        public async Task<bool> CreateDefaultRoleAsync()
        {
            try
            {
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransactionAsync())
                    {
                        LaboteUser user = new LaboteUser();

                        if (_roleManager.Roles.Count() == 0)
                        {

                            var userRol = _roleManager.CreateAsync(
                                new UserRole
                                {
                                    Name = "SuperAdmin",
                                    NormalizedName = "SuperAdmin",
                                    NotDelete = true,
                                }
                                ).Result;
                        }
                    };

                }
            }
            catch (Exception e)
            {

                throw;
            }



            return true;
        }




    }
}
