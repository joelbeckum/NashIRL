USE [NashIRL]
GO

set identity_insert [UserType] on
INSERT INTO [UserType] ([Id], [Type]) VALUES (1, 'Admin'), (2, 'User');
set identity_insert [UserType] off

set identity_insert [UserProfile] on
INSERT INTO [UserProfile] ([Id], [FirebaseUserId], [FirstName], [LastName], [Email], [UserTypeId]) VALUES (1, 'aDjtzsL5pBXArUKrAXGyZAbhOKQ2', 'Joel', 'Beckum', 'joel@email.com', 1);
INSERT INTO [UserProfile] ([Id], [FirebaseUserId], [FirstName], [LastName], [Email], [UserTypeId]) VALUES (2, 'jUneNetwGKbMQBIwaOg1BRIikJC2', 'Mike', 'Woods', 'mike@email.com', 1);
INSERT INTO [UserProfile] ([Id], [FirebaseUserId], [FirstName], [LastName], [Email], [UserTypeId]) VALUES (3, 'klzWHYwLO3X1GN5igFlJFYVNrh12', 'Zach', 'Richards', 'zach@email.com', 2);
set identity_insert [UserProfile] off

set identity_insert [Hobby] on
INSERT INTO [Hobby] ([Id], [Name], [IsApproved]) VALUES (1, 'Hiking', 1);
INSERT INTO [Hobby] ([Id], [Name], [IsApproved]) VALUES (2, 'Breweries', 1);
INSERT INTO [Hobby] ([Id], [Name], [IsApproved]) VALUES (3, 'Disc Golf', 1);
set identity_insert [Hobby] off

set identity_insert [Event] on
INSERT INTO [Event] ([Id], [Name], [Description], [CreatedOn], [EventOn], [UserProfileId], [HobbyId]) VALUES (1, 'Golf at Crockett Part', 'We are gonna get together and play 18 at Crockett. Beginners welcome!', '2021-10-2', '2021-10-15', 1, 3);
INSERT INTO [Event] ([Id], [Name], [Description], [CreatedOn], [EventOn], [UserProfileId], [HobbyId]) VALUES (2, 'Bold Patriot Party', 'Several of us are meeting at Bold Patriot to celebrate finishing a big project.', '2021-10-4', '2021-10-18', 2, 2);
INSERT INTO [Event] ([Id], [Name], [Description], [CreatedOn], [EventOn], [UserProfileId], [HobbyId]) VALUES (3, 'Hike at Percy Warner', 'Meet up at Percy Warner Park at 8 for a nice early hike', '2021-8-12', '2021-8-20', 1, 1);
INSERT INTO [Event] ([Id], [Name], [Description], [CreatedOn], [EventOn], [UserProfileId], [HobbyId]) VALUES (4, 'Check out Liberty Park', 'I hear this course is bananas!', '2021-9-12', '2021-9-18', 3, 3);
set identity_insert [Event] off

set identity_insert [Tag] on
INSERT INTO [Tag] ([Id], [Name], [IsApproved]) VALUES (1, 'Newcomers Welcome', 1);
INSERT INTO [Tag] ([Id], [Name], [IsApproved]) VALUES (2, 'Free', 1);
INSERT INTO [Tag] ([Id], [Name], [IsApproved]) VALUES (3, 'Kid-friendly', 1);
set identity_insert [Tag] off

set identity_insert [Comment] on
INSERT INTO [Comment] ([Id], [Body], [UserProfileId], [EventId], [CreatedOn]) VALUES (1, 'Sounds great!', 2, 1, 2021-10-3);
INSERT INTO [Comment] ([Id], [Body], [UserProfileId], [EventId], [CreatedOn]) VALUES (2, 'See you there.', 3, 1, 2021-10-3);
INSERT INTO [Comment] ([Id], [Body], [UserProfileId], [EventId], [CreatedOn]) VALUES (3, 'Sorry guys, I''m not going to make it', 2, 2, 2021-10-10);
INSERT INTO [Comment] ([Id], [Body], [UserProfileId], [EventId], [CreatedOn]) VALUES (4, 'Hmm, do you think we''ll need sunscreen?', 3, 4, 2021-9-12);
set identity_insert [Comment] off