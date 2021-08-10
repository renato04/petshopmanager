using PetShop.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Web.UI.Services
{
    public class MenuService
    {
        Menu[] allMenus = new[] {
            new Menu()
            {
                Name = "First Look",
                Path = "/",
                Icon = "&#xe88a"
            },
            new Menu()
            {
                Name = "Dashboard",
                Path = "/dashboard",
                Icon = "&#xe871"
            },
            new Menu
            {
                Name = "Clientes",
                Title = "Administre os seus clientes",
                Path = "/client",
                Icon = "&#xe037"
            },
            new Menu
            {
                Name = "Administração",
                Title = "Administre os usuários da aplicação",
                Path = "/administrador",
                Icon = "&#xe037"
            },

        };

        public IEnumerable<Menu> Menus
        {
            get
            {
                return allMenus;
            }
        }

        public IEnumerable<Menu> Filter(string term)
        {
            Func<string, bool> contains = value => value.Contains(term, StringComparison.OrdinalIgnoreCase);

            Func<Menu, bool> filter = (Menu) => contains(Menu.Name) || (Menu.Tags != null && Menu.Tags.Any(contains));

            return Menus.Where(category => category.Children != null && category.Children.Any(filter))
                           .Select(category => new Menu()
                           {
                               Name = category.Name,
                               Expanded = true,
                               Children = category.Children.Where(filter).ToArray()
                           }).ToList();
        }

        public Menu FindCurrent(Uri uri)
        {
            return Menus.SelectMany(Menu => Menu.Children ?? new[] { Menu })
                           .FirstOrDefault(Menu => Menu.Path == uri.AbsolutePath || $"/{Menu.Path}" == uri.AbsolutePath);
        }

        public string TitleFor(Menu Menu)
        {
            if (Menu != null && Menu.Name != "First Look")
            {
                return Menu.Title ?? $"Blazor {Menu.Name} | a free UI component by Radzen";
            }

            return "Free Blazor Components | 60+ controls by Radzen";
        }

        public string DescriptionFor(Menu Menu)
        {
            return Menu?.Description ?? "The Radzen Blazor component library provides more than 50 UI controls for building rich ASP.NET Core web applications.";
        }
    }
}
