using LiteDB;
using NKnife.IoC;

namespace NKnife.DataLite.UnitTest.SingleDatabase
{
    public abstract class GlobalDataBaseRepositoryBase<T, TId> : CrudRepositoryBase<T, TId>
    {

        protected GlobalDataBaseRepositoryBase()
        {
        }

        public LiteDatabase DatabaseToTest
        {
            get { return _Database; }
        }

        #region Overrides of RepositoryBase<ExhibitData<T>>

        protected override LiteDatabase Database => DatabaseService.Instance.DataBase;

        #endregion
    }
}