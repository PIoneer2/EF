﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF.Core.Data;
using EF.Data;
using EF.Core;
using System.Runtime.CompilerServices;
using EF.Web.Models;
using System.Web.Mvc;

namespace EF.Web.Models
{
    public static class BL
    {
        public static object CreateBlankModel<T>(object transRep, long UserId) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {
                
                EFRepository<Transactions> transactionsRepository = (EFRepository<Transactions>)transRep;
                Transactions model = new Transactions();
                model.Date = System.DateTime.Now;
                model.Description = "";
                model.TranactionTypeId = 1;
                model.UserId = UserId;
                transactionsRepository.Insert(model);
                return model;
            }
            else
            {
                //заглушка
                EFRepository<T> tmpRepository = (EFRepository<T>)transRep;
                T model = tmpRepository.GetById(0);
                return model;
            }
        }

        public static object CreateEditInPost<T>(object mdl, object transRep, long UserId) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {

                Transactions model = (Transactions)mdl;
                EFRepository<Transactions> transactionsRepository = (EFRepository<Transactions>)transRep;
               
                var editModel = transactionsRepository.GetById(model.Id);
                    editModel.Description = model.Description;
                    editModel.TranactionTypeId = model.TranactionTypeId;
                    editModel.UserId = model.UserId;
                    editModel.Date = model.Date;
                    transactionsRepository.Update(editModel);
              
                return model;
            }

            if (typeof(T) == typeof(Goods))
            {
                Goods model = (Goods)mdl;
                EFRepository<Goods> goodsRepository = (EFRepository<Goods>)transRep;
                if (model.Id == 0)
                {
                    model.Name = "";
                    model.Quantity = 1;
                    model.Info = "";
                    goodsRepository.Insert(model);
                }
                else
                {
                    var editModel = goodsRepository.GetById(model.Id);
                    editModel.Name = model.Name; ;
                    editModel.Quantity = model.Quantity;
                    editModel.Info = model.Info;
                    goodsRepository.Update(editModel);
                }

                return model;
            }

            //заглушка для компилятора
            else {
                string error = "Wrong type at CreateEditInPost<T> creation";
                return error;
            }
        }

        public static object CreateEdit<T>(object transRep, long id = 0) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {
                EFRepository<T> tmpRepository = (EFRepository<T>)transRep;
                T model = tmpRepository.GetById(id);
                return model;
            }
            else
            {
                //заглушка для компилятора
                string error = "Wrong type at CreateEdit<T> creation";
                return error;
            }
        }

        public static object Index<T>(object transRep, long UserId = 0) where T : class, IBaseEntity
        {
            EFRepository<T> tmpRepository = (EFRepository<T>)transRep;
            List<T> allEntities = tmpRepository.Table.ToList();
            if (UserId != 0)
            {
                if (typeof(T) == typeof(Transactions))
                {
                    List<Transactions> selectedEntities = allEntities.OfType<Transactions>().ToList();
                    List<Transactions> finalEntities = selectedEntities.Where(m => m.UserId == UserId).ToList();
                    return finalEntities;
                }
            }
            return allEntities;
        }

        public static object Details<T>(long id, object transRep) where T : class, IBaseEntity
        {
            EFRepository<T> tmpRepository = (EFRepository<T>)transRep;
            T model = tmpRepository.GetById(id);
            return model;
        }

        public static void ConfirmDelete<T>(long id, object transRep) where T : class, IBaseEntity
        {
            EFRepository<T> tmpRepository = (EFRepository<T>)transRep;
            T model = tmpRepository.GetById(id);
            tmpRepository.Delete(model);
        }
    }
}