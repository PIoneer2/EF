using System.Collections.Generic;
using System.Linq;
using EF.Core.Data;
using EF.Core;
using System;
using EF.Web.SLocator;

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
                    List<Transactions> finalEntities = (allEntities as List<Transactions>).Where(m => m.UserId == id).ToList();//selectedEntities.Where(m => m.UserId == id).ToList();
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

            if (typeof(T) == typeof(Goods))
            {
                IRepository<Goods> goodsRepository = (IRepository<Goods>)parametricRepository;
                Goods model = new Goods();
                model.Name = "Good Name";
                model.Quantity = 0;
                model.TypeOfStorageId = 1;
                model.SizesId = 1;
                model.Info = "";
                goodsRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(GoodsInTransaction))
            {
                IRepository<GoodsInTransaction> goodsInTransactionRepository = (IRepository<GoodsInTransaction>)parametricRepository;
                GoodsInTransaction model = new GoodsInTransaction();
                model.Quantity = 0;
                model.TransactionsId = 1;
                model.GoodsId = 1;
                goodsInTransactionRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(GoodsInWarehauses))
            {
                IRepository<GoodsInWarehauses> goodsInWarehausesRepository = (IRepository<GoodsInWarehauses>)parametricRepository;
                GoodsInWarehauses model = new GoodsInWarehauses();
                model.GoodsId = 1;
                model.WarehousesPlacesId = 1;
                goodsInWarehausesRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(Restrictions))
            {
                IRepository<Restrictions> restrictionsRepository = (IRepository<Restrictions>)parametricRepository;
                Restrictions model = new Restrictions();
                model.RestrictionName = "RestrictionName";
                restrictionsRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(RestrictionsSet))
            {
                IRepository<RestrictionsSet> restrictionsSetRepository = (IRepository<RestrictionsSet>)parametricRepository;
                RestrictionsSet model = new RestrictionsSet();
                model.RestrictionsId = 1;
                model.GoodsId = 1;
                restrictionsSetRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(Role))
            {
                IRepository<Role> roleRepository = (IRepository<Role>)parametricRepository;
                Role model = new Role();
                model.Name = "RoleName";
                roleRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(Sizes))
            {
                IRepository<Sizes> sizesRepository = (IRepository<Sizes>)parametricRepository;
                Sizes model = new Sizes();
                model.Size = "SizeDescription";
                sizesRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(TranactionType))
            {
                IRepository<TranactionType> tranactionTypeRepository = (IRepository<TranactionType>)parametricRepository;
                TranactionType model = new TranactionType();
                model.Name = "TranactionTypeName";
                tranactionTypeRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(TypeOfStorage))
            {
                IRepository<TypeOfStorage> typeOfStorageRepository = (IRepository<TypeOfStorage>)parametricRepository;
                TypeOfStorage model = new TypeOfStorage();
                model.Type = "TypeOfStorageDescription";
                typeOfStorageRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(User))
            {
                IRepository<User> userRepository = (IRepository<User>)parametricRepository;
                User model = new User();
                model.AccessFailedCount = 0;
                model.Email = "0@1.com";
                model.EmailConfirmed = false;
                model.LockoutEnabled = false;
                model.PhoneNumberConfirmed = false;
                model.TwoFactorEnabled = false;
                model.UserName = model.Email;
                userRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(UserRole))
            {
                IRepository<UserRole> userRoleRepository = (IRepository<UserRole>)parametricRepository;
                UserRole model = new UserRole();
                model.RoleId = 1;
                model.UserId = id;
                userRoleRepository.Insert(model);
                return model as T;
            }

            if (typeof(T) == typeof(WarehousesPlaces))
            {
                IRepository<WarehousesPlaces> warehousesPlacesRepository = (IRepository<WarehousesPlaces>)parametricRepository;
                WarehousesPlaces model = new WarehousesPlaces();
                model.Adress = "Kharkiv";
                model.Place = "1";
                warehousesPlacesRepository.Insert(model);
                return model as T;
            }

            else
            {
                //заглушка
                throw new NotImplementedException();
            }
        }

        T IBusinessLogic.EditInPost<T>(T mdl, IRepository<T> parametricRepository)
        {
            if (typeof(T) == typeof(Transactions))
            {
                Transactions model = mdl as Transactions;
                IRepository<Transactions> tmpRepository = (IRepository<Transactions>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Description = model.Description;
                editModel.TranactionTypeId = model.TranactionTypeId;
                editModel.UserId = model.UserId;
                editModel.Date = model.Date;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(Goods))
            {
                Goods model = mdl as Goods;
                IRepository<Goods> tmpRepository = (IRepository<Goods>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Name = model.Name; ;
                editModel.Quantity = model.Quantity;
                editModel.Info = model.Info;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(GoodsInTransaction))
            {
                GoodsInTransaction model = mdl as GoodsInTransaction;
                IRepository<GoodsInTransaction> tmpRepository = (IRepository<GoodsInTransaction>)parametricRepository;

                var editModel = tmpRepository.GetById(model.Id);

                if (editModel.Transactions != null)
                {
                    if (editModel.Transactions.TranactionTypeId == 1)
                    {
                        editModel.Goods.Quantity -= editModel.Quantity;
                        editModel.Goods.Quantity += model.Quantity;
                    }
                    if (editModel.Transactions.TranactionTypeId == 2)
                    {
                        editModel.Goods.Quantity += editModel.Quantity;
                        editModel.Goods.Quantity -= model.Quantity;
                    }
                    if (editModel.Goods.Quantity <0)
                    {
                        throw new Exception("Code tried make Goods.Quantity <0");
                    }
                }
                else
                {
                    IUnitOfWork unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
                    IRepository<Goods> goodsRepository = goodsRepository = unitOfWork.Repository<Goods>();
                    IRepository<Transactions> transactionsRepository = unitOfWork.Repository<Transactions>();
                    var goodsModel = goodsRepository.GetById(model.TransactionsId);
                    var transactionsModel = transactionsRepository.GetById(model.GoodsId);

                    if (transactionsModel.TranactionTypeId == 1)
                    {
                        goodsModel.Quantity += model.Quantity;
                    }
                    if (transactionsModel.TranactionTypeId == 2)
                    {
                        goodsModel.Quantity -= model.Quantity;
                    }
                    if (goodsModel.Quantity < 0)
                    {
                        throw new Exception("Code tried make Goods.Quantity <0");
                    }
                    goodsRepository.Update(goodsModel);
                }

                editModel.Quantity = model.Quantity;
                editModel.TransactionsId = model.TransactionsId;
                editModel.GoodsId = model.GoodsId;
                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(GoodsInWarehauses))
            {
                GoodsInWarehauses model = mdl as GoodsInWarehauses;
                IRepository<GoodsInWarehauses> tmpRepository = (IRepository<GoodsInWarehauses>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.GoodsId = model.GoodsId; ;
                editModel.WarehousesPlacesId = model.WarehousesPlacesId;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(Restrictions))
            {
                Restrictions model = mdl as Restrictions;
                IRepository<Restrictions> tmpRepository = (IRepository<Restrictions>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.RestrictionName = model.RestrictionName; ;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(RestrictionsSet))
            {
                RestrictionsSet model = mdl as RestrictionsSet;
                IRepository<RestrictionsSet> tmpRepository = (IRepository<RestrictionsSet>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.RestrictionsId = model.RestrictionsId;
                editModel.GoodsId = model.GoodsId;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(Role))
            {
                Role model = mdl as Role;
                IRepository<Role> tmpRepository = (IRepository<Role>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Name = model.Name;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(Sizes))
            {
                Sizes model = mdl as Sizes;
                IRepository<Sizes> tmpRepository = (IRepository<Sizes>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Size = model.Size;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(TranactionType))
            {
                TranactionType model = mdl as TranactionType;
                IRepository<TranactionType> tmpRepository = (IRepository<TranactionType>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Name = model.Name;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(TypeOfStorage))
            {
                TypeOfStorage model = mdl as TypeOfStorage;
                IRepository<TypeOfStorage> tmpRepository = (IRepository<TypeOfStorage>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Type = model.Type;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(User))
            {
                User model = mdl as User;
                IRepository<User> tmpRepository = (IRepository<User>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.AccessFailedCount = model.AccessFailedCount;
                editModel.Email = model.Email;
                editModel.EmailConfirmed = model.EmailConfirmed;
                editModel.LockoutEnabled = model.LockoutEnabled;
                editModel.LockoutEndDateUtc = model.LockoutEndDateUtc;
                editModel.PasswordHash = model.PasswordHash;
                editModel.PhoneNumber = model.PhoneNumber;
                editModel.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
                editModel.SecurityStamp = model.SecurityStamp;
                editModel.TwoFactorEnabled = model.TwoFactorEnabled;
                editModel.UserName = model.UserName;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(UserRole))
            {
                UserRole model = mdl as UserRole;
                IRepository<UserRole> tmpRepository = (IRepository<UserRole>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.RoleId = model.RoleId;
                editModel.UserId = model.UserId;

                tmpRepository.Update(editModel);
                return model as T;
            }

            if (typeof(T) == typeof(WarehousesPlaces))
            {
                WarehousesPlaces model = mdl as WarehousesPlaces;
                IRepository<WarehousesPlaces> tmpRepository = (IRepository<WarehousesPlaces>)parametricRepository;
                var editModel = tmpRepository.GetById(model.Id);

                editModel.Adress = model.Adress;
                editModel.Place = model.Place;

                tmpRepository.Update(editModel);
                return model as T;
            }

            else
            {
                throw new NotImplementedException();
            }
        }

        void IBusinessLogic.ConfirmDelete<T>(IRepository<T> parametricRepository, long id)
        {
            if (typeof(T) == typeof(GoodsInTransaction))
            {
                IRepository<GoodsInTransaction> tmpRepository = (IRepository<GoodsInTransaction>)parametricRepository;
                GoodsInTransaction model = tmpRepository.GetById(id);
                if (model.Transactions.TranactionTypeId == 1)
                {
                    model.Goods.Quantity -= model.Quantity;
                }
                if (model.Transactions.TranactionTypeId == 2)
                {
                    model.Goods.Quantity += model.Quantity;
                }
                tmpRepository.Delete(model);
            }
            else
            {
                IRepository<T> tmpRepository = parametricRepository;
                T model = tmpRepository.GetById(id);
                tmpRepository.Delete(model);
            }
        }

        TransactionDTO IBusinessLogic.FromBaseToDTOTransaction(Transactions fromObject)
        { 
                TransactionDTO toObjectTmp = EFServiceLocator.GetService<TransactionDTO>();

                toObjectTmp.Date = fromObject.Date;
                toObjectTmp.Description = fromObject.Description;
                toObjectTmp.TranactionTypeId = fromObject.TranactionTypeId;
                toObjectTmp.UserId = fromObject.UserId;
                toObjectTmp.Id = fromObject.Id;

                return toObjectTmp;
        }

            void IBusinessLogic.FromDTOtoBaseClass(BaseEntity fromObject, IBaseEntity toObject, bool copyId)
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

            if ((toObject.GetType() == typeof(Goods)) && (fromObject.GetType() == typeof(GoodDTO)))
            {
                Goods toObjectTmp = (Goods)toObject;
                GoodDTO fromObjectTmp = (GoodDTO)fromObject;
                toObjectTmp.Name = fromObjectTmp.Name;
                toObjectTmp.TypeOfStorageId = fromObjectTmp.TypeOfStorageId;
                toObjectTmp.SizesId = fromObjectTmp.SizesId;
                toObjectTmp.Info = fromObjectTmp.Info;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(GoodsInTransaction)) && (fromObject.GetType() == typeof(GoodsInTransactionDTO)))
            {
                GoodsInTransaction toObjectTmp = (GoodsInTransaction)toObject;
                GoodsInTransactionDTO fromObjectTmp = (GoodsInTransactionDTO)fromObject;
                toObjectTmp.Quantity = fromObjectTmp.Quantity;
                toObjectTmp.TransactionsId = fromObjectTmp.TransactionsId;
                toObjectTmp.GoodsId = fromObjectTmp.GoodsId;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(GoodsInWarehauses)) && (fromObject.GetType() == typeof(GoodsInWarehausesDTO)))
            {
                GoodsInWarehauses toObjectTmp = (GoodsInWarehauses)toObject;
                GoodsInWarehausesDTO fromObjectTmp = (GoodsInWarehausesDTO)fromObject;
                toObjectTmp.GoodsId = fromObjectTmp.GoodsId;
                toObjectTmp.WarehousesPlacesId = fromObjectTmp.WarehousesPlacesId;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(Restrictions)) && (fromObject.GetType() == typeof(RestrictionsDTO)))
            {
                Restrictions toObjectTmp = (Restrictions)toObject;
                RestrictionsDTO fromObjectTmp = (RestrictionsDTO)fromObject;
                toObjectTmp.RestrictionName = fromObjectTmp.RestrictionName;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(RestrictionsSet)) && (fromObject.GetType() == typeof(RestrictionsSetDTO)))
            {
                RestrictionsSet toObjectTmp = (RestrictionsSet)toObject;
                RestrictionsSetDTO fromObjectTmp = (RestrictionsSetDTO)fromObject;
                toObjectTmp.RestrictionsId = fromObjectTmp.RestrictionsId;
                toObjectTmp.GoodsId = fromObjectTmp.GoodsId;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(Role)) && (fromObject.GetType() == typeof(RoleDTO)))
            {
                Role toObjectTmp = (Role)toObject;
                RoleDTO fromObjectTmp = (RoleDTO)fromObject;
                toObjectTmp.Name = fromObjectTmp.Name;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(Sizes)) && (fromObject.GetType() == typeof(SizesDTO)))
            {
                Sizes toObjectTmp = (Sizes)toObject;
                SizesDTO fromObjectTmp = (SizesDTO)fromObject;
                toObjectTmp.Size = fromObjectTmp.Size;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(TranactionType)) && (fromObject.GetType() == typeof(TranactionTypeDTO)))
            {
                TranactionType toObjectTmp = (TranactionType)toObject;
                TranactionTypeDTO fromObjectTmp = (TranactionTypeDTO)fromObject;
                toObjectTmp.Name = fromObjectTmp.Name;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(User)) && (fromObject.GetType() == typeof(UserDTO)))
            {
                User toObjectTmp = (User)toObject;
                UserDTO fromObjectTmp = (UserDTO)fromObject;
                toObjectTmp.AccessFailedCount = fromObjectTmp.AccessFailedCount;
                toObjectTmp.Email = fromObjectTmp.Email;
                toObjectTmp.EmailConfirmed = fromObjectTmp.EmailConfirmed;
                toObjectTmp.LockoutEnabled = fromObjectTmp.LockoutEnabled;
                toObjectTmp.LockoutEndDateUtc = fromObjectTmp.LockoutEndDateUtc;
                toObjectTmp.PasswordHash = fromObjectTmp.PasswordHash;
                toObjectTmp.PhoneNumber = fromObjectTmp.PhoneNumber;
                toObjectTmp.PhoneNumberConfirmed = fromObjectTmp.PhoneNumberConfirmed;
                toObjectTmp.SecurityStamp = fromObjectTmp.SecurityStamp;
                toObjectTmp.TwoFactorEnabled = fromObjectTmp.TwoFactorEnabled;
                toObjectTmp.UserName = fromObjectTmp.UserName;
                if (copyId)
                {
                    toObjectTmp.Id = fromObjectTmp.Id;
                }
                toObject = toObjectTmp;
                fromObject = fromObjectTmp;
            }

            if ((toObject.GetType() == typeof(UserRole)) && (fromObject.GetType() == typeof(UserRoleDTO)))
            {
                UserRole toObjectTmp = (UserRole)toObject;
                UserRoleDTO fromObjectTmp = (UserRoleDTO)fromObject;
                toObjectTmp.RoleId = fromObjectTmp.RoleId;
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