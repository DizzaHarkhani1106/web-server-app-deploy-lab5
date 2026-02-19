USE [VehicleInventoryDb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_Vehicle]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_Vehicle] DROP CONSTRAINT IF EXISTS [FK_DH_Vehicle_DH_VehicleType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_Inventory]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_Inventory] DROP CONSTRAINT IF EXISTS [FK_DH_Inventory_DH_VehicleStatus]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_Inventory]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_Inventory] DROP CONSTRAINT IF EXISTS [FK_DH_Inventory_DH_VehicleLocation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_Inventory]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_Inventory] DROP CONSTRAINT IF EXISTS [FK_DH_Inventory_DH_Vehicle]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_Inventory]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_Inventory] DROP CONSTRAINT IF EXISTS [DF__DH_Inventory__LastU__74AE54BC]
GO
/****** Object:  Index [UQ__DH_VehicleT__737584F61253212D]    Script Date: 2026-02-03 11:11:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_VehicleType]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_VehicleType] DROP CONSTRAINT IF EXISTS [UQ__DH_VehicleT__737584F61253212D]
GO
/****** Object:  Index [UQ__DH_VehicleS__737584F697EFE4BF]    Script Date: 2026-02-03 11:11:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_VehicleStatus]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_VehicleStatus] DROP CONSTRAINT IF EXISTS [UQ__DH_VehicleS__737584F697EFE4BF]
GO
/****** Object:  Index [UQ__DH_VehicleL__737584F65980E5BE]    Script Date: 2026-02-03 11:11:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DH_VehicleLocation]') AND type in (N'U'))
ALTER TABLE [dbo].[DH_VehicleLocation] DROP CONSTRAINT IF EXISTS [UQ__DH_VehicleL__737584F65980E5BE]
GO
/****** Object:  Table [dbo].[DH_VehicleType]    Script Date: 2026-02-03 11:11:41 PM ******/
DROP TABLE IF EXISTS [dbo].[DH_VehicleType]
GO
/****** Object:  Table [dbo].[DH_VehicleStatus]    Script Date: 2026-02-03 11:11:41 PM ******/
DROP TABLE IF EXISTS [dbo].[DH_VehicleStatus]
GO
/****** Object:  Table [dbo].[DH_VehicleLocation]    Script Date: 2026-02-03 11:11:41 PM ******/
DROP TABLE IF EXISTS [dbo].[DH_VehicleLocation]
GO
/****** Object:  Table [dbo].[DH_Vehicle]    Script Date: 2026-02-03 11:11:41 PM ******/
DROP TABLE IF EXISTS [dbo].[DH_Vehicle]
GO
/****** Object:  Table [dbo].[DH_Inventory]    Script Date: 2026-02-03 11:11:41 PM ******/
DROP TABLE IF EXISTS [dbo].[DH_Inventory]
GO
/****** Object:  Table [dbo].[DH_Inventory]    Script Date: 2026-02-03 11:11:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DH_Inventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[VehicleLocationId] [int] NOT NULL,
	[VehicleStatusId] [int] NOT NULL,
	[LastUpdated] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DH_Vehicle]    Script Date: 2026-02-03 11:11:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DH_Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[VehicleTypeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DH_VehicleLocation]    Script Date: 2026-02-03 11:11:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DH_VehicleLocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DH_VehicleStatus]    Script Date: 2026-02-03 11:11:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DH_VehicleStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DH_VehicleType]    Script Date: 2026-02-03 11:11:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DH_VehicleType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DH_Inventory] ON 
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (1, 1, 1, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (2, 2, 1, 2, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (3, 3, 2, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (4, 4, 2, 3, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (5, 5, 3, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (6, 6, 3, 4, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (7, 1, 4, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (8, 2, 4, 2, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (9, 3, 1, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[DH_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (10, 4, 2, 3, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[DH_Inventory] OFF
GO
SET IDENTITY_INSERT [dbo].[DH_Vehicle] ON 
GO
INSERT [dbo].[DH_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (1, N'Toyota', N'Camry', 1)
GO
INSERT [dbo].[DH_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (2, N'Honda', N'Civic', 1)
GO
INSERT [dbo].[DH_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (3, N'Ford', N'Escape', 2)
GO
INSERT [dbo].[DH_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (4, N'Toyota', N'RAV4', 2)
GO
INSERT [dbo].[DH_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (5, N'Ford', N'F-150', 3)
GO
INSERT [dbo].[DH_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (6, N'Chevy', N'Express', 4)
GO
SET IDENTITY_INSERT [dbo].[DH_Vehicle] OFF
GO
SET IDENTITY_INSERT [dbo].[DH_VehicleLocation] ON 
GO
INSERT [dbo].[DH_VehicleLocation] ([Id], [Name]) VALUES (3, N'Cambridge')
GO
INSERT [dbo].[DH_VehicleLocation] ([Id], [Name]) VALUES (4, N'Guelph')
GO
INSERT [dbo].[DH_VehicleLocation] ([Id], [Name]) VALUES (1, N'Kitchener')
GO
INSERT [dbo].[DH_VehicleLocation] ([Id], [Name]) VALUES (2, N'Waterloo')
GO
SET IDENTITY_INSERT [dbo].[DH_VehicleLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[DH_VehicleStatus] ON 
GO
INSERT [dbo].[DH_VehicleStatus] ([Id], [Name]) VALUES (1, N'Available')
GO
INSERT [dbo].[DH_VehicleStatus] ([Id], [Name]) VALUES (4, N'Maintenance')
GO
INSERT [dbo].[DH_VehicleStatus] ([Id], [Name]) VALUES (3, N'Rented')
GO
INSERT [dbo].[DH_VehicleStatus] ([Id], [Name]) VALUES (2, N'Reserved')
GO
SET IDENTITY_INSERT [dbo].[DH_VehicleStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[DH_VehicleType] ON 
GO
INSERT [dbo].[DH_VehicleType] ([Id], [Name]) VALUES (1, N'Sedan')
GO
INSERT [dbo].[DH_VehicleType] ([Id], [Name]) VALUES (2, N'SUV')
GO
INSERT [dbo].[DH_VehicleType] ([Id], [Name]) VALUES (3, N'Truck')
GO
INSERT [dbo].[DH_VehicleType] ([Id], [Name]) VALUES (4, N'Van')
GO
SET IDENTITY_INSERT [dbo].[DH_VehicleType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__DH_VehicleL__737584F65980E5BE]    Script Date: 2026-02-03 11:11:41 PM ******/
ALTER TABLE [dbo].[DH_VehicleLocation] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__DH_VehicleS__737584F697EFE4BF]    Script Date: 2026-02-03 11:11:41 PM ******/
ALTER TABLE [dbo].[DH_VehicleStatus] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__DH_VehicleT__737584F61253212D]    Script Date: 2026-02-03 11:11:41 PM ******/
ALTER TABLE [dbo].[DH_VehicleType] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DH_Inventory] ADD  DEFAULT (sysdatetime()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[DH_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_DH_Inventory_DH_Vehicle] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[DH_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[DH_Inventory] CHECK CONSTRAINT [FK_DH_Inventory_DH_Vehicle]
GO
ALTER TABLE [dbo].[DH_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_DH_Inventory_DH_VehicleLocation] FOREIGN KEY([VehicleLocationId])
REFERENCES [dbo].[DH_VehicleLocation] ([Id])
GO
ALTER TABLE [dbo].[DH_Inventory] CHECK CONSTRAINT [FK_DH_Inventory_DH_VehicleLocation]
GO
ALTER TABLE [dbo].[DH_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_DH_Inventory_DH_VehicleStatus] FOREIGN KEY([VehicleStatusId])
REFERENCES [dbo].[DH_VehicleStatus] ([Id])
GO
ALTER TABLE [dbo].[DH_Inventory] CHECK CONSTRAINT [FK_DH_Inventory_DH_VehicleStatus]
GO
ALTER TABLE [dbo].[DH_Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_DH_Vehicle_DH_VehicleType] FOREIGN KEY([VehicleTypeId])
REFERENCES [dbo].[DH_VehicleType] ([Id])
GO
ALTER TABLE [dbo].[DH_Vehicle] CHECK CONSTRAINT [FK_DH_Vehicle_DH_VehicleType]
GO
