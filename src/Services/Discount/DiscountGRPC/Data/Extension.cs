﻿using Microsoft.EntityFrameworkCore;

namespace DiscountGRPC.Data
{
    public static class Extension
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app) 
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
