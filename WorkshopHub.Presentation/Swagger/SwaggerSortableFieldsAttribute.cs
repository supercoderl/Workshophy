﻿namespace WorkshopHub.Presentation.Swagger
{
    public abstract class SwaggerSortableFieldsAttribute : Attribute
    {
        public abstract IEnumerable<string> GetFields();
    }
}
