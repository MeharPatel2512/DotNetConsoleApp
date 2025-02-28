using System.Data;

namespace ConsoleApp.Repositories{
    public interface IExecuteStoredProcedure{
        public DataSet CallStoredProcedure(string StoredProcedure, Dictionary<string, object>? Parameters);
    }
}