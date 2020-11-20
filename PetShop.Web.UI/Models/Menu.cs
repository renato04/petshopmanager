using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Web.UI.Models
{
    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Expanded { get; set; }
        public IEnumerable<Menu> Children { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
