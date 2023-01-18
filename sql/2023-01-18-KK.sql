CREATE PROCEDURE usp_GetContacts
AS
BEGIN
SELECT c.[Name], c.[Surname], STRING_AGG(pn.[PrefixNumber] + pn.[Number], ', ') AS PhoneNumbers
FROM Contacts c
LEFT JOIN PhoneNumbers pn on c.Id_Contact = pn.Contact_Id
GROUP BY c.[Name], c.[Surname] 
ORDER BY c.[Surname] 

END
