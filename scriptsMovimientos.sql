USE [Practice]
GO
/****** Object:  Table [dbo].[TipoEmpleado]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEmpleado](
	[Id] [int] NOT NULL,
	[Tipo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grupo]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[Id] [char](1) NOT NULL,
	[Grupo] [varchar](100) NULL,
	[IdTipoEmpleado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[Id] [int] NOT NULL,
	[NombreEmpleado] [varchar](100) NULL,
	[IdGrupo] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articulo]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[IdTipoArticulo] [int] NULL,
	[UnidadTalla] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimiento]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdArticulo] [int] NULL,
	[Talla] [varchar](10) NULL,
	[Fecha] [datetime] NULL,
	[TipoMovimiento] [varchar](10) NULL,
	[Cantidad] [int] NULL,
	[Motivo] [varchar](100) NULL,
	[IdEmpleado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_MovimientosEmpleado]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_MovimientosEmpleado] AS
SELECT 
	M.Id,
    M.Fecha,
    M.TipoMovimiento,
    M.Cantidad,
    M.Talla,
    A.Descripcion AS Articulo,
    E.NombreEmpleado,
    G.grupo AS Grupo,
    TE.TIPO AS TipoEmpleado
FROM Movimiento M
JOIN Articulo A ON M.IdArticulo = A.Id
LEFT JOIN Empleado E ON M.IdEmpleado = E.Id
LEFT JOIN Grupo G ON E.IdGrupo = G.Id
LEFT JOIN TipoEmpleado TE ON G.IdTipoEmpleado = TE.Id;
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdArticulo] [int] NULL,
	[Talla] [varchar](10) NULL,
	[StockActual] [int] NULL,
	[StockMinimo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoArticulo]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoArticulo](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Aplicacion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Articulo] ([Id], [Descripcion], [IdTipoArticulo], [UnidadTalla]) VALUES (1, N'Lentes de protección', 5, N'UNITALLA')
INSERT [dbo].[Articulo] ([Id], [Descripcion], [IdTipoArticulo], [UnidadTalla]) VALUES (2, N'Lentes de Sol', 5, N'MX')
INSERT [dbo].[Articulo] ([Id], [Descripcion], [IdTipoArticulo], [UnidadTalla]) VALUES (4, N'Martillo', 6, N'UNITALLA')
GO
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (473545, N'VICTOR MANUEL AGUILAR VARGAS', N'A')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (474415, N'JUAN CIENFUEGOS PIMENTEL', N'C')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (474654, N'RUBEN JIMENEZ DE LA ROSA', N'Z')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (474945, N'FIDEL GONZALEZ GONZALEZ', N'A')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480710, N'ALEXIS JAVIER CORCUERA SOLIS', N'Z')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480766, N'ELADIO HINOJOSA HERNANDEZ', N'A')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480788, N'SAUL  CARPACIO TRINIDAD', N'A')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480805, N'LUIS FERNANDO GALLEGOS ARELLANO', N'Z')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480827, N'ARNOLDO  SANCHEZ LUNA', N'B')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480843, N'EDGAR OSWALDO LARA GAVIÑO', N'Z')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480864, N'OMAR JONATHAN  QUIROZ CARDENAS', N'A')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480928, N'HECTOR MIGUEL GOMEZ FLORES', N'C')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480951, N'DENZEL SEBASTIAN MOJICA GODOY', N'D')
INSERT [dbo].[Empleado] ([Id], [NombreEmpleado], [IdGrupo]) VALUES (480952, N'CARLOS LOPEZ JIMENEZ', N'C')
GO
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'A', N'MANTENIMIENTO DE MAQUINARIA', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'B', N'CABULLEROS BUQUE', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'C', N'TRACTOS', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'D', N'PATIO', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'E', N'ALMACEN', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'F', N'DESARROLLO SOFTWARE', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'G', N'TRANSPORTE MAQUINARIA', 1)
INSERT [dbo].[Grupo] ([Id], [Grupo], [IdTipoEmpleado]) VALUES (N'Z', N'ADMINISTRATIVOS', 2)
GO
SET IDENTITY_INSERT [dbo].[Movimiento] ON 

INSERT [dbo].[Movimiento] ([Id], [IdArticulo], [Talla], [Fecha], [TipoMovimiento], [Cantidad], [Motivo], [IdEmpleado]) VALUES (2, 2, N'XS', CAST(N'2025-06-20T12:20:56.973' AS DateTime), N'SALIDA', 1, N'Entrega a trabajador', 473545)
INSERT [dbo].[Movimiento] ([Id], [IdArticulo], [Talla], [Fecha], [TipoMovimiento], [Cantidad], [Motivo], [IdEmpleado]) VALUES (3, 1, N'M', CAST(N'2025-06-20T13:20:47.737' AS DateTime), N'SALIDA', 2, N'Entrega a trabajador', 480951)
INSERT [dbo].[Movimiento] ([Id], [IdArticulo], [Talla], [Fecha], [TipoMovimiento], [Cantidad], [Motivo], [IdEmpleado]) VALUES (4, 4, N'NA', CAST(N'2025-06-20T13:25:10.427' AS DateTime), N'SALIDA', 1, N'Entrega a trabajador', 480805)
SET IDENTITY_INSERT [dbo].[Movimiento] OFF
GO
INSERT [dbo].[TipoArticulo] ([Id], [Descripcion], [Aplicacion]) VALUES (2, N'Proteccion para la cabeza', N'AMBOS')
INSERT [dbo].[TipoArticulo] ([Id], [Descripcion], [Aplicacion]) VALUES (3, N'Proteccion para las manos', N'AMBOS')
INSERT [dbo].[TipoArticulo] ([Id], [Descripcion], [Aplicacion]) VALUES (4, N'Proteccion de los pies', N'AMBOS')
INSERT [dbo].[TipoArticulo] ([Id], [Descripcion], [Aplicacion]) VALUES (5, N'Proteccion para los ojos', N'AMBOS')
INSERT [dbo].[TipoArticulo] ([Id], [Descripcion], [Aplicacion]) VALUES (6, N'Herramientas de mano', N'AMBOS')
INSERT [dbo].[TipoArticulo] ([Id], [Descripcion], [Aplicacion]) VALUES (7, N'Herramientas de impacto', N'CONFIANZA')
GO
INSERT [dbo].[TipoEmpleado] ([Id], [Tipo]) VALUES (1, N'SINDICALIZADOS')
INSERT [dbo].[TipoEmpleado] ([Id], [Tipo]) VALUES (2, N'CONFIANZA')
GO
ALTER TABLE [dbo].[Movimiento] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Articulo]  WITH CHECK ADD FOREIGN KEY([IdTipoArticulo])
REFERENCES [dbo].[TipoArticulo] ([Id])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupo] ([Id])
GO
ALTER TABLE [dbo].[Grupo]  WITH CHECK ADD FOREIGN KEY([IdTipoEmpleado])
REFERENCES [dbo].[TipoEmpleado] ([Id])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([IdArticulo])
REFERENCES [dbo].[Articulo] ([Id])
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD FOREIGN KEY([IdArticulo])
REFERENCES [dbo].[Articulo] ([Id])
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleado] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[SP_MOVIMIENTOS_REGISTRAR_ENTREGA]    Script Date: 20/06/2025 01:34:11 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MOVIMIENTOS_REGISTRAR_ENTREGA]
    @Id_Empleado INT,
    @Id_Articulo INT,
    @Talla VARCHAR(10),
    @Cantidad INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Registrar salida
    INSERT INTO Movimiento (IdArticulo, Talla, TipoMovimiento, Cantidad, Motivo, IdEmpleado)
    VALUES (@Id_Articulo, @Talla, 'SALIDA', @Cantidad, 'Entrega a trabajador', @Id_Empleado);

    -- Actualizar inventario
    UPDATE Inventario
    SET StockActual = StockActual - @Cantidad
    WHERE IdArticulo = @Id_Articulo AND Talla = @Talla;
END;
GO
