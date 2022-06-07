using ProjectPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPortfolio.ViewModels
{
    public class HomeVM
    {
        public List<About> Abouts { get; set; }
        public List<Award> Awards { get; set; }
        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Tools> Tools { get; set; }
        public List<Workflow> Workflows { get; set; }
        public List<Work> Works { get; set; }
    }
}
