using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ApplicationUser : IdentityUser
    {
        public List<Bookmark> Bookmarks { get; set; }
        public List<Category> Categories { get; set; }
    }
}
