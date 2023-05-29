INSERT INTO dbo.[User]([Email], [FirstName], [LastName], [Password])
/* the passwords are set to "password" but are hashed*/
VALUES 
    ('example@gmail.com', 'John', 'Rodriguez', 'gLIj2HltE+aKwgStTAvjrTeOpR1k0zlOu1lSkN6TlT4=:ZTSEZ6nvpQ8g28bHs2gFyg==:10000:SHA256        ')