using Raven.Client.Documents;
using System.Reflection.Metadata.Ecma335;

namespace Rvn.Ch02;

public static class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> store = new(CreateStore);

    public static IDocumentStore _store => store.Value;
    private static IDocumentStore CreateStore()
    {
        IDocumentStore store = new DocumentStore()
        {
            Urls = new[] { "http://localhost:8080" },


            Conventions =
            {
                MaxNumberOfRequestsPerSession = 10,
                UseOptimisticConcurrency = true,

            },
            Database = "Tasks",

        }.Initialize();
    
        return store;
    }

}
