using System.Collections.Generic;
using System.Linq;
using EF.Core.Data;
using EF.Core;
using System;

namespace EF.Web.BusinessLogic
{
    public class EFBusinessLogic : IBusinessLogic
    {
        /// <summary>
        /// Business logic method for selecting list of T of set UserId. If UserId = 0, then no filtering.
        /// </summary>
        /// <typeparam name="T">Type is one of IBaseType</typeparam>
        /// <param name="parametricRepository">IRepository<T> with your query</param>
        /// <param name="id">UserId. If "0" - returns full query, if != "0" - returns records with set User</param>
        /// <returns>List<T></returns>
        IEnumerable<T> IBusinessLogic.Index<T>(IRepository<T> parametricRepository, long id)
        {
            IRepository<T> tmpRepository = (IRepository<T>)parametricRepository;
            List<T> allEntities = tmpRepository.Table.ToList();
            if (id != 0)
            {
                if (typeof(T) == typeof(Transactions))
                {
                    List<Transactions> selectedEntities = allEntities.OfType<Transactions>().ToList();
                    List<Transactions> finalEntities = selectedEntities.Where(m => m.UserId == id).ToList();
                    return finalEntities as IEnumerable<T>;
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

        void IBusinessLogic.Transform(IBaseEntity toObject, BaseEntity fromObject, bool copyId = false)
        {
            if ((toObject.GetType() == typeof(Transactions)) && (fromObject.GetType() == typeof(TransactionDTO)))
            {
                Transactions toObjectTmp = (Transactions)toObject;
                TransactionDTO fromObjectTmp = (TransactionDTO)fromObject;
                toObjectTmp.Date = fromObjectTmp.Date;
                toObjectTmp.Description = fromObjectTmp.Description;
                toObjectTmp.TranactionTypeId = fromObjectTmp.TranactionTypeId;
                toObjectTmp.UserId = fromObjectTmp.UserId;
                if (copyId)
                { 
                toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }
        }
    }
}