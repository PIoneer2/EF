using System.Collections.Generic;
using System.Linq;
using EF.Core.Data;
using EF.Core;
using System;

namespace EF.Web.BusinessLogic
{
    public class EFBusinessLogic : IBusinessLogic
    {/*
        public object CreateBlankModel<T>(object transRep, long UserId) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {
                
                IRepository<Transactions> transactionsRepository = (IRepository<Transactions>)transRep;
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
                IRepository<T> tmpRepository = (IRepository<T>)transRep;
                T model = tmpRepository.GetById(0);
                return model;
            }
        }*/

        /*
        public object CreateEditInPost<T>(object mdl, object transRep, long UserId) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {

                Transactions model = (Transactions)mdl;
                IRepository<Transactions> transactionsRepository = (IRepository<Transactions>)transRep;
               
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
                IRepository<Goods> goodsRepository = (IRepository<Goods>)transRep;
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
        }*/
        /*
        public object CreateEdit<T>(object transRep, long id = 0) where T : class, IBaseEntity
        {
            if (typeof(T) == typeof(Transactions))
            {
                IRepository<T> tmpRepository = (IRepository<T>)transRep;
                T model = tmpRepository.GetById(id);
                return model;
            }
            else
            {
                //заглушка для компилятора
                string error = "Wrong type at CreateEdit<T> creation";
                return error;
            }
        }*/

        /*
        public object Index<T>(object transRep, long UserId = 0) where T : class, IBaseEntity
        {
            IRepository<T> tmpRepository = (IRepository<T>)transRep;
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
        }*/
        /*
        public object Details<T>(long id, object transRep) where T : class, IBaseEntity
        {
            IRepository<T> tmpRepository = (IRepository<T>)transRep;
            T model = tmpRepository.GetById(id);
            return model;
        }*/
        /*
        public void ConfirmDelete<T>(long id, object transRep) where T : class, IBaseEntity
        {
            IRepository<T> tmpRepository = (IRepository<T>)transRep;
            T model = tmpRepository.GetById(id);
            tmpRepository.Delete(model);
        }*/

        object IBusinessLogic.Index<T>(IRepository<T> parametricRepository, long id)
        {
            IRepository<T> tmpRepository = (IRepository<T>)parametricRepository;
            List<T> allEntities = tmpRepository.Table.ToList();
            if (id != 0)
            {
                if (typeof(T) == typeof(Transactions))
                {
                    List<Transactions> selectedEntities = allEntities.OfType<Transactions>().ToList();
                    List<Transactions> finalEntities = selectedEntities.Where(m => m.UserId == id).ToList();
                    return finalEntities;
                }
            }
            return allEntities;
        }

        T IBusinessLogic.Details<T>(IRepository<T> parametricRepository, long id)
        {
            IRepository<T> tmpRepository = (IRepository<T>)parametricRepository;
            T model = tmpRepository.GetById(id);
            return model;
        }

        T IBusinessLogic.CreateBlankModel<T>(IRepository<T> parametricRepository, long id)
        {
            if (typeof(T) == typeof(Transactions))
            {

                IRepository<Transactions> transactionsRepository = (IRepository<Transactions>)parametricRepository;
                Transactions model = new Transactions();
                model.Date = System.DateTime.Now;
                model.Description = "";
                model.TranactionTypeId = 1;
                model.UserId = id;
                transactionsRepository.Insert(model);
                return model as T;
            }
            else
            {
                //заглушка
                throw new NotImplementedException();
            }
        }

        T IBusinessLogic.CreateEditInPost<T>(T mdl, IRepository<T> parametricRepository, long id)
        {
            if (typeof(T) == typeof(Transactions))
            {

                Transactions model = mdl as Transactions;
                IRepository<Transactions> transactionsRepository = (IRepository<Transactions>)parametricRepository;

                var editModel = transactionsRepository.GetById(model.Id);
                editModel.Description = model.Description;
                editModel.TranactionTypeId = model.TranactionTypeId;
                editModel.UserId = model.UserId;
                editModel.Date = model.Date;
                transactionsRepository.Update(editModel);

                return model as T;
            }

            if (typeof(T) == typeof(Goods))
            {
                Goods model = mdl as Goods;
                IRepository<Goods> goodsRepository = (IRepository<Goods>)parametricRepository;
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

                return model as T;
            }

            //заглушка для компилятора
            else
            {
                throw new NotImplementedException();
            }
        }

        void IBusinessLogic.ConfirmDelete<T>(IRepository<T> parametricRepository, long id)
        {
            IRepository<T> tmpRepository = parametricRepository;
            T model = tmpRepository.GetById(id);
            tmpRepository.Delete(model);
        }
    }
}