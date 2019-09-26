using EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DaoBase<T> where T : class
    {
        public static MyDbContext CreateContext()
        {
            MyDbContext db = CallContext.GetData("s") as MyDbContext;
            if (db == null)
            {
                db = new MyDbContext();
                CallContext.SetData("s", db);
            }
            return db;
        }

        //分离
        public void FenLi(T t)
        {
            //1 创建ObjectDbContext对象
            MyDbContext db = CreateContext();
            var ObjContext = ((IObjectContextAdapter)db).ObjectContext;

            //2 创建新的 ObjectSet< TEntity > 实例
            var objSet = ObjContext.CreateObjectSet<T>();
            //3 为特定对象创建实体键，如果实体键已存在，则返回该键。
            var objKey = ObjContext.CreateEntityKey(objSet.EntitySet.Name, t);
            //4 返回具有指定实体键的对象。
            object objT;
            var ext = ObjContext.TryGetObjectByKey(objKey, out objT);
            //5 从对象上下文移除对象。
            if (ext)
            {

                ObjContext.Detach(objT);
            }
        }

        //增
        public int Add(T t)
        {
            MyDbContext db = CreateContext();
            db.Set<T>().Add(t);
            return db.SaveChanges();
        }

        //删
        public int Delete(T t)
        {
            MyDbContext db = CreateContext();
            FenLi(t);
            db.Set<T>().Attach(t);
            db.Entry<T>(t).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();

        }

        //改
        public int Update(T t)
        {

            MyDbContext db = CreateContext();
            FenLi(t);
            db.Set<T>().Attach(t);
            db.Entry<T>(t).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        //查询全部
        public List<T> Select()
        {

            return CreateContext().Set<T>()
                   .AsNoTracking()
                   .Select(e => e)
                   .ToList();
        }
    }
}
