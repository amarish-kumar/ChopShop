using System;
using System.ComponentModel.DataAnnotations;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditCategory
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Localisation.ViewModels.EditCategory))]
        [StringLength(255, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(Localisation.ViewModels.EditCategory))]
        public string Name { get; set; }

        public string Description { get; set; }
        public bool IsInProduct { get; set; }

        public void FromEntity(Category categoryEntity)
        {
            Id = categoryEntity.Id;
            Name = categoryEntity.Name;
            Description = categoryEntity.Description;
        }

        public Category ToEntity()
        {
            var category = new Category
                               {
                                   Id = this.Id,
                                   Name = this.Name,
                                   Description = this.Description
                               };

            return category;
        }
    }
}