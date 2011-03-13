using System;
using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInProduct { get; set; }

        public void FromEntity(Category categoryEntity)
        {
            Id = categoryEntity.Id;
            Name = categoryEntity.Name;
            Description = categoryEntity.Description;
        }
    }
}