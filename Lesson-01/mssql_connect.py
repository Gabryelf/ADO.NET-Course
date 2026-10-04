import mssql_python

str_connect = (
    "Server=COMP7A2\\SQLEXPRESS;"
    "Database=TestIndex2;"
    "Trusted_Connection=yes;"
    "Encrypt=yes;"
    "TrustServerCertificate=yes"
)
connection = mssql_python.connect(str_connect)

cursor = connection.cursor()


sql_command = "SELECT TOP 10 * FROM dbo.orders2"
cursor.execute(sql_command)

rows = cursor.fetchall()

for row in rows:
    print(row)
