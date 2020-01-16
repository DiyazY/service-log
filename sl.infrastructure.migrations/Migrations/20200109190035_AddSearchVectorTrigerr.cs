using Microsoft.EntityFrameworkCore.Migrations;

namespace sl.infrastructure.migrations.Migrations
{
    public partial class AddSearchVectorTrigerr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION update_s_vector() RETURNS trigger AS $$
              BEGIN
                new.s_vector :=
                  to_tsvector('pg_catalog.english', coalesce(new.message,'')) ||
                  to_tsvector('pg_catalog.english', coalesce(new.system_id,'')) ||
                  to_tsvector('pg_catalog.english', coalesce(new.level,'')) ||
                  array_to_tsvector(new.labels);
                return new;
              END
              $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
            CREATE TRIGGER log_search_vector_update BEFORE INSERT OR UPDATE
              ON log FOR EACH ROW EXECUTE PROCEDURE update_s_vector();
            ");

            migrationBuilder.Sql("UPDATE log SET message = message;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER log_search_vector_update ON log;");
            migrationBuilder.Sql(@"DROP FUNCTION update_s_vector();");
        }
    }
}
