import pyodbc

connection_string = (
    "DRIVER={ODBC Driver 18 for SQL Server};"
    "SERVER=(localdb)\\MSSQLLocalDB;"
    "DATABASE=TraineeDB;"
    "Trusted_Connection=yes;"
    "TrustServerCertificate=yes;"
)


def connect_start():
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute("SELECT TOP 5 * FROM Students")

    for row in cursor:
        print(row)

    conn.close()


def create_table():
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()

    cursor.execute("""
        IF OBJECT_ID('dbo.Students', 'U') IS NULL
        CREATE TABLE dbo.Students (
            StudentId INT IDENTITY(1,1) PRIMARY KEY,
            FirstName NVARCHAR(50) NOT NULL,
            LastName  NVARCHAR(50) NOT NULL,
            Age       INT NULL
        )
    """)
    conn.commit()


def insert_table():
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute("""
        INSERT INTO dbo.Students (FirstName, LastName, Age)
        VALUES (?, ?, ?)
    """, "Иван", "Иванов", 20)
    conn.commit()


create_table()

insert_table()

connect_start()
