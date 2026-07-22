using Marten.Schema;

namespace Catalog.API.Data
{
	public class CatalogInitialData : IInitialData
	{
		public async Task Populate(IDocumentStore store, CancellationToken cancellationToken)
		{
			using var session = store.LightweightSession();
			if (await session.Query<Product>().AnyAsync())
				return;

			// Marten UPSERT will cater for existing records
			session.Store<Product>();
			await session.SaveChangesAsync();
		}
	}
}
