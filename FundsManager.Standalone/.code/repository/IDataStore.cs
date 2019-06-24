namespace FundsManager.Standalone {
    public interface IDataStore {
        Symbol.Data.IDataContext DataContext { get; }
        decimal TotalInMoney { get; }
        decimal TotalOutMoney { get; }

        void Add(ConsumeType model);
        void Edit(ConsumeType model);
        void Remove(ConsumeType model);
        bool ExistsConsumeTypeWithName(string name, long id);
        bool HasRefencenConsumeType(long id);

        Symbol.Data.IDataQuery<ConsumeType> FindAllConsumeType(string columns=null);

        void Add(FundsLog model);
        void Edit(FundsLog model);
        void Remove(FundsLog model);
        FundsLog FindFundsLog(long id);
        Paging<FundsLog> FindAllFundsLog(int page, System.DateTime? beginDate = null, System.DateTime? endDate = null, long? consumeTypeId = null, string keyword=null);

        User Register(string account, string password, string name);
        User FindUserByAccount(string account);
        bool ExistsUserWithAccount(string account);
        bool ExistsUserWithName(string name);
        void ChangePassword(string account, string newPassword);

        decimal GetTotalMoneyByMonth(int year, int month,bool isOut);
        decimal GetTotalMoneyByConsumeType(long consumeTypeId, System.DateTime? beginDate = null, System.DateTime? endDate = null);

        void CheckSchema();

        void upload_LastMoney_ConsumeType();
        void upload_LastMoney_FundsLog();
    }

    public class Paging<T> {
        public Symbol.Data.IDataQuery<T> Query { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
    
}