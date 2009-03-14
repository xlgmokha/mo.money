using Db4objects.Db4o;
using MoMoney.Infrastructure.Extensions;
using MoMoney.Presentation.Model.Projects;

namespace MoMoney.DataAccess.db40
{
    public interface IConnectionFactory
    {
        IObjectContainer open_connection_to(IFile the_path_to_the_database_file);
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public IObjectContainer open_connection_to(IFile the_path_to_the_database_file)
        {
            this.log().debug("open connection to {0}", the_path_to_the_database_file.path);
            return Db4oFactory.OpenFile(the_path_to_the_database_file.path);
        }
    }
}