 protected IEnumerable<T> thisToList<T>(IDbCommand command) where T : class
        {
            command.CommandTimeout = 0;
            try
            {
                using (var record = command.ExecuteReader())
                {
                    List<T> items = new List<T>();
                    while (record.Read())
                    {
                        items.Add(Map<T>(record));
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
