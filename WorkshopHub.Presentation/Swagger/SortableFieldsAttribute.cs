using WorkshopHub.Application.ViewModels.Sorting;

namespace WorkshopHub.Presentation.Swagger
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class SortableFieldsAttribute<TSortingProvider, TViewModel, TEntity>
        : SwaggerSortableFieldsAttribute
        where TSortingProvider : ISortingExpressionProvider<TViewModel, TEntity>, new()
    {
        public override IEnumerable<string> GetFields()
        {
            return new TSortingProvider().GetSortingExpressions().Keys;
        }
    }
}
