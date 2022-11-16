### 

```C#
 public DataTable GetDataTableTotal(DataTable items)
        {
            try
            {
                DataRow totalsRow = items.NewRow();
                for (int i = 0; i < items.Columns.Count; i++)
                {
                    try
                    {
                        string columnName = items.Columns[i].ColumnName.ToString();
                        totalsRow[i] = items.AsEnumerable().Sum(row => (row.Field<decimal>(columnName)));
                    }
                    catch (Exception)
                    {
                    }
                }
                items.Rows.Add(totalsRow);
                
            }
            catch (Exception ex)
            {
                
            }
            return items;
        }
```
