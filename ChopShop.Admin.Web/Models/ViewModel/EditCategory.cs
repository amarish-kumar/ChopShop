using System;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditCategory
    {
        public Guid Id { get; set; }
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