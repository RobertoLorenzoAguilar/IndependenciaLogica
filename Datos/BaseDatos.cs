using System;  // C# , ADO.NET  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT = System.Data;            // System.Data.dll  
using QC = System.Data.SqlClient;  // System.Data.dll  
namespace Datos
{
    public class BaseDatos
    {
        static public List<string> Main()
        {
            try
            {
                //using (var connection = new QC.SqlConnection(
                // "Server=tcp:DESKTOP-IFCBOE4;" +
                //   "Database=Independencia;User ID=rob1;" +
                //   "Password=santiago;Encrypt=True;" +
                //   "TrustServerCertificate=true;Connection Timeout=30;"//truestserverCertificate debe estar tipo true
                //    )
                //    )

                using (var connection = new QC.SqlConnection(
                        "Server=DESKTOP-IFCBOE4\\SQLEXPRESS; Database=Independencia;Integrated Security= True;"))

                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");

                    return BaseDatos.SelectRows(connection);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        static public List<string> SelectRows(QC.SqlConnection connection)
        {
            using (var command = new QC.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = DT.CommandType.Text;
                command.CommandText = @"  
                select * from INFORMATION_SCHEMA.COLUMNS  where TABLE_NAME='tbl_Usuario'";
                //command.CommandText = @"  
                //select * from tbl_Usuario";

                QC.SqlDataReader reader = command.ExecuteReader();
                List<string> lstatrUsuario = new List<string>();
                while (reader.Read())
                {
                    lstatrUsuario.Add(reader[3].ToString());//retorna los campos de usuario
                }
                return lstatrUsuario;
            }
        }
    }
}




//use Independencia

//select
//    d.object_id,
//    a.name[table], -- identificara la Tabla
//    b.name[column], -- identificara la columna
//    c.name[type], -- identificara el Tipo
//    CASE-- recibe el tipo de columna
//	  --cuando c es numerico  o c es     decimal o  c es      Float entonces se precisa el numero

//        WHEN c.name = 'numeric' OR c.name = 'decimal' OR c.name = 'float'  THEN b.precision
//        ELSE null

//    END[Precision],
//--  recibe maximo tamaño de b
//    b.max_length,
//    CASE -- recibe si la columna acepta nulos

//        WHEN b.is_nullable = 0 THEN 'NO'

//        ELSE 'SI'

//    END[Permite Nulls],
//    CASE -- recibe si la columna es identity (autoincrementable)
//        WHEN b.is_identity = 0 THEN 'NO'

//        ELSE 'SI'

//    END[Es Autonumerico],
//    ep.value[Descripcion],-- recibe la descripcion de la columna(si la hay)

//    f.ForeignKey, -- recibe si es llave foranea
//    f.ReferenceTableName, -- recibe la referencia de la tabla

//    f.ReferenceColumnName -- recibe la referencia de la columna
//from sys.tables a   
//      --          //    Seleciona y muestra toda la informacion   \\          --

//    inner join sys.columns b on a.object_id= b.object_id

//    inner join sys.systypes c on b.system_type_id= c.xtype

//    inner join sys.objects d on a.object_id= d.object_id

//    LEFT JOIN sys.extended_properties ep ON d.object_id = ep.major_id AND b.column_Id = ep.minor_id

//    LEFT JOIN (SELECT
//                f.name AS ForeignKey,
//                OBJECT_NAME(f.parent_object_id) AS TableName,
//                COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
//                OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
//                COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName
//                FROM sys.foreign_keys AS f
//                INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id)  f ON f.TableName =a.name AND f.ColumnName =b.name
//WHERE a.name<> 'sysdiagrams' 
//ORDER BY a.name,b.column_Id
