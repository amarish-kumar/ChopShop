using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public List<EditCategory> CategoryList(IEnumerable<Category> allCategories, Guid productId, IEnumerable<Category> allCategoriesForProduct = null)
        {
            if (allCategories != null  && allCategories.Any())
            {
                var categoryList = allCategories.Select(x => new EditCategory { Id = x.Id, Name = x.Name, Description = x.Description })
                                                .OrderBy(x => x.Name)
                                                .ToList();

                if (allCategoriesForProduct != null && allCategoriesForProduct.Any())
                {
                    categoryList.ForEach(x =>
                                             {
                                                 if (allCategoriesForProduct.FirstOrDefault(y => y.Id == x.Id) != null)
                                                 {
                                                     x.IsInProduct = true;
                                                 }
                                             });
                }

                return categoryList;
            }
            return null;
        }
    }
}