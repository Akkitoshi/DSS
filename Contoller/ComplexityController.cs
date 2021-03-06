﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModels;

namespace Contoller
{
    public class ComplexityController
    {
        private DBCourseWorkContext context;

        public ComplexityController(DBCourseWorkContext context)
        {
            this.context = context;
        }

        // список всех товаров
        public List<ComplexityView> GetList()
        {
            List<ComplexityView> result = context.Complexities.Select(rec => new
           ComplexityView
            {
                Id = rec.Id,
                Name = rec.Name
            })
            .ToList();
            return result;
        }
        // добавление нового товара
        public void AddElement(Complexity model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Complexity element = context.Complexities.FirstOrDefault(rec =>
                   rec.Id == model.Id);
                    if (element != null)
                    {
                        throw new Exception("Элемент с данным идентификатором уже существует");
                    }
                    element = new Complexity
                    {
                        Id = model.Id,
                        Name = model.Name
                    };
                    context.Complexities.Add(element);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        //удалить продукт
        public void delElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Complexity element = context.Complexities.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        context.Complexities.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
