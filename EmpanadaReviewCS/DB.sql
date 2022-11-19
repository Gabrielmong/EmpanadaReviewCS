USE [EmpanadaReview]
GO
/****** Object:  Table [dbo].[Dish]    Script Date: 11/19/22 3:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dish](
	[idDish] [int] IDENTITY(1,1) NOT NULL,
	[idRestaurant] [int] NOT NULL,
	[title] [varchar](50) NOT NULL,
	[description] [varchar](1024) NOT NULL,
	[price] [int] NOT NULL,
	[createdAt] [date] NOT NULL,
	[updatedAt] [date] NULL,
	[imageSrc] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDish] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drink]    Script Date: 11/19/22 3:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drink](
	[idDrink] [int] IDENTITY(1,1) NOT NULL,
	[idRestaurant] [int] NOT NULL,
	[title] [varchar](50) NOT NULL,
	[description] [varchar](1024) NOT NULL,
	[price] [int] NOT NULL,
	[createdAt] [date] NOT NULL,
	[updatedAt] [date] NULL,
	[imageSrc] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDrink] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 11/19/22 3:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[idRating] [int] IDENTITY(1,1) NOT NULL,
	[score] [int] NOT NULL,
	[createdAt] [date] NOT NULL,
	[updatedAt] [date] NULL,
	[idRestaurant] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRating] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 11/19/22 3:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurant](
	[idRestaurant] [int] IDENTITY(1,1) NOT NULL,
	[location] [varchar](50) NOT NULL,
	[averageRating] [int] NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](1024) NOT NULL,
	[createdAt] [date] NOT NULL,
	[updatedAt] [date] NULL,
	[imageSrc] [varchar](200) NULL,
	[foodType] [varchar](50) NULL,
	[hasAllergies] [int] NULL,
	[restrictions] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idRestaurant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 11/19/22 3:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[idReview] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [int] NOT NULL,
	[idRating] [int] NOT NULL,
	[title] [varchar](30) NOT NULL,
	[description] [varchar](1024) NOT NULL,
	[createdAt] [date] NOT NULL,
	[updatedAt] [date] NULL,
	[imageSrc] [varchar](200) NULL,
	[idRestaurant] [int] NULL,
	[likes] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idReview] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEmpanada]    Script Date: 11/19/22 3:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEmpanada](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](15) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[firstName] [varchar](15) NOT NULL,
	[lastName] [varchar](15) NOT NULL,
	[bio] [varchar](500) NULL,
	[createdAt] [date] NOT NULL,
	[updatedAt] [date] NULL,
	[imageSrc] [varchar](200) NULL,
	[phoneNumber] [varchar](11) NULL,
	[email] [varchar](320) NULL,
	[gender] [varchar](20) NULL,
	[reviews] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Dish]  WITH CHECK ADD FOREIGN KEY([idRestaurant])
REFERENCES [dbo].[Restaurant] ([idRestaurant])
GO
ALTER TABLE [dbo].[Drink]  WITH CHECK ADD FOREIGN KEY([idRestaurant])
REFERENCES [dbo].[Restaurant] ([idRestaurant])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([idRestaurant])
REFERENCES [dbo].[Restaurant] ([idRestaurant])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([idRating])
REFERENCES [dbo].[Rating] ([idRating])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([idUser])
REFERENCES [dbo].[UserEmpanada] ([idUser])
GO
