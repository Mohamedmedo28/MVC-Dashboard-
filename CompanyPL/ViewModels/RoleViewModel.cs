using System;

namespace CompanyPL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        //لما حد يخلق اوبجيكت من RoleViewModel هيبدا انه يسكن id ب guid
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
