SELECT [Id_Contact], [Name], [Surname], STRING_AGG([PrefixNumber] + [Number], ', ') AS [PhoneNumbers]
FROM Contacts c
INNER JOIN PhoneNumbers pn on pn.Contact_Id = c.Id_Contact
GROUP BY [Id_Contact], [Name], [Surname]

SELECT [Id_Contact], [Name], [Surname], [PrefixNumber] + [Number] AS [PhoneNumbers]
FROM Contacts c
INNER JOIN PhoneNumbers pn on pn.Contact_Id = c.Id_Contact