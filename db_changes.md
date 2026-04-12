# SQL - модель C# 

---

## Добавить столбец

```sql
-- строка (фикс. длина / юникод)
ALTER TABLE Products ADD Code CHAR(10) NULL;
ALTER TABLE Products ADD Title NVARCHAR(200) NULL;

-- числа
ALTER TABLE Products ADD Qty INT NULL;

-- дата/время
ALTER TABLE Products ADD CreatedAt DATETIME2 NULL;

-- логический (в T-SQL bit)
ALTER TABLE Products ADD IsActive BIT NOT NULL CONSTRAINT DF_Products_IsActive DEFAULT(0);
```

**Модель**

```csharp
public string? Title { get; set; }
public int? Qty { get; set; }
public DateTime? CreatedAt { get; set; }
public bool IsActive { get; set; }
```

---

## Удалить столбец

```sql
ALTER TABLE Products DROP COLUMN OldField;
```

**Модель:** свойство убрать из класса и из UI/запросов.

---

## Переименовать столбец

```sql
EXEC sp_rename 'Products.OldName', 'NewName', 'COLUMN';
```

**Модель:** либо свойство `NewName`, либо оставить имя в C# и указать столбец:

```csharp
[Column("NewName")]
public string? OldName { get; set; }
```

---

## Изменить NULL / NOT NULL

```sql
-- разрешить NULL
ALTER TABLE Products ALTER COLUMN Comment NVARCHAR(500) NULL;

-- запретить NULL (сначала заполни пустые значения)
UPDATE Products SET Comment = N'' WHERE Comment IS NULL;
ALTER TABLE Products ALTER COLUMN Comment NVARCHAR(500) NOT NULL;
```

**Модель:** `string?` ↔ NULL; `string` или `string` без `?` + инициализация ↔ NOT NULL (лучше явно `required` / конструктор при необходимости).

---

## Изменить тип столбца

```sql
-- пример: INT → BIGINT
ALTER TABLE Products ALTER COLUMN Qty BIGINT NULL;

-- NVARCHAR(50) → NVARCHAR(200) (расширение обычно без проблем)
ALTER TABLE Products ALTER COLUMN Title NVARCHAR(200) NULL;
```

**Модель:** тип свойства согласовать (`long?` для BIGINT, `string`/`string?` для NVARCHAR и т.д.).

---

## Связь имён БД и C#

| SQL (часто)   | C#                         |
|---------------|----------------------------|
| `NVARCHAR`    | `string` / `string?`       |
| `INT`         | `int` / `int?`             |
| `BIGINT`      | `long` / `long?`           |
| `DECIMAL`     | `decimal` / `decimal?`     |
| `BIT`         | `bool` / `bool?`           |
| `DATETIME2`   | `DateTime` / `DateTime?`   |

Столбец `Комментарий` в БД, свойство `Comment` в коде:

```csharp
[Column("Комментарий")]
public string? Comment { get; set; }
```
