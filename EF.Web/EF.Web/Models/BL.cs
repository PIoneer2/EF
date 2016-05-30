using System;
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
        public static object CreateEditInPost<T>(object mdl, object transRep) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {
                Transactions model = (Transactions)mdl;
                EFRepository<Transactions> transactionsRepository = (EFRepository<Transactions>)transRep;
                if (0 == model.Id)
                {
                    model.Date = System.DateTime.Now;
                    model.Description = "";
                    model.TranactionTypeId = 1;
                    model.UserId = 0;
                    transactionsRepository.Insert(model);
                }

                else
                {
                    var editModel = transactionsRepository.GetById(model.Id);
                    editModel.Description = model.Description; ;
                    editModel.TranactionTypeId = model.TranactionTypeId;
                    editModel.UserId = model.UserId;
                    editModel.Date = model.Date;
                    transactionsRepository.Update(editModel);
                }
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
                string error = "Wrong type at creation";
                return error;
            }
        }

        public static object Index<T>(object transRep, long UserId = 0) where T : class, IBaseEntity
        {
            EFRepository<T> tmpRepository = (EFRepository<T>)transRep;
            IEnumerable<T> typedEntities = tmpRepository.Table.ToList();
            
            if (UserId != 0)
            {
                //выбор транзакций по ИД пользователя, который запрашивает выборку
                //List<T> Entities = typedEntities.ToListAsync();
                //Entities = Entities.Where(e => e.User.Id == UserId);//выборка в репозитории работает только по long Id
            }
            return typedEntities;
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