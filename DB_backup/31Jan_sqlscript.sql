USE [db_JalaunKisanKhadMgmtSys]
GO

CREATE TABLE [dbo].[tbl_FertilizerStockTransactionsLog](
	[TransactionID] [int] IDENTITY(100,1) NOT NULL,
	[FertilizerID] [int] NOT NULL,
	[TransactionTypeStatusId] [int] NOT NULL,
	[Quantity] [decimal](10, 2) NOT NULL,
	[TransactionDate] [datetime] NULL,
	[Remarks] [varchar](255) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[SocietyId] [int] NULL,
	[DemandDetailsId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_FertilizerStockTransactionsLog] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO

ALTER TABLE [dbo].[tbl_FertilizerStockTransactionsLog] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[tbl_FertilizerStockTransactionsLog] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


USE [db_JalaunKisanKhadMgmtSys]
GO

/****** Object:  StoredProcedure [dbo].[sp_getSocietyFertilizerDetails]    Script Date: 1/31/2026 8:15:10 AM ******/
DROP PROCEDURE [dbo].[sp_getSocietyFertilizerDetails]
GO

/****** Object:  StoredProcedure [dbo].[sp_SaveUpdateGetFertilizerStock]    Script Date: 1/31/2026 8:15:10 AM ******/
DROP PROCEDURE [dbo].[sp_SaveUpdateGetFertilizerStock]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAvlStockByFertilizer]    Script Date: 1/31/2026 8:15:10 AM ******/
DROP PROCEDURE [dbo].[sp_GetAvlStockByFertilizer]
GO

/****** Object:  StoredProcedure [dbo].[SP_DistributeFertilizerToFarmer]    Script Date: 1/31/2026 8:15:10 AM ******/
DROP PROCEDURE [dbo].[SP_DistributeFertilizerToFarmer]
GO

/****** Object:  StoredProcedure [dbo].[SP_DistributeFertilizerToFarmer]    Script Date: 1/31/2026 8:15:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- SP_DistributeFertilizerToFarmer
CREATE   PROCEDURE [dbo].[SP_DistributeFertilizerToFarmer]
(
    @DemandDetailsId INT,
    @FertilizerId INT,
    @DistributeQty DECIMAL(18,3),
    @SocietyId INT,
    @CreatedBy INT,
    @Remarks VARCHAR(255) = NULL,
	@VendorId int=null
)
AS
BEGIN
   -- SET NOCOUNT ON;
   DECLARE @StatusId int =0;
   set @StatusId=(select top 1  StatusId 
				from M_StatusMaster 
				where StatusType='FertilizerDemand' and StatusNameEnglish='Distribution to Farmer'and IsActive=1)

	DECLARE @DistributedMT DECIMAL(18,6);
    SET @DistributedMT = @DistributeQty / 1000.0;


    BEGIN TRAN;

    BEGIN TRY

        DECLARE 
            @AvailableStock DECIMAL(18,3),
            @DemandQty DECIMAL(18,3),
            @ReceivedQty DECIMAL(18,3),
            @RemainingDemand DECIMAL(18,3);



        --  Current Stock
        SELECT top 1 @AvailableStock =   CurrentStock --- M.T
        FROM M_FertilizerStock
        WHERE FertilizerID = @FertilizerId
          AND SocietyId = @SocietyId
          AND IsActive = 1
		  order by StockId desc

        IF (@AvailableStock < @DistributedMT)
            THROW 50001, 'Insufficient stock available', 1;

        --  Demand Details
        SELECT 
            @DemandQty = FertilizerNeed,
            @ReceivedQty = ISNULL(RecieveQty,0)
        FROM  [dbo].[tbl_FertilizerDemendDetails]
        WHERE Id = @DemandDetailsId;

        SET @RemainingDemand = @DemandQty - @ReceivedQty;

        IF (@RemainingDemand < @DistributeQty)
            THROW 50002, 'Distribution exceeds remaining demand', 1;

        --  Update Demand Received Qty
        UPDATE  [dbo].[tbl_FertilizerDemendDetails]
        SET RecieveQty = ISNULL(RecieveQty,0) + @DistributeQty,
		DistributerVendorId=@VendorId
        WHERE Id = @DemandDetailsId;

        --  Reduce Stock
		
		;with ctestock as (
		select top 1 CurrentStock,StockId,ISNULL(UsedQty,0)UsedQty
		from M_FertilizerStock
		WHERE FertilizerID = @FertilizerId
			AND SocietyId = @SocietyId
			order by stockid desc
			)


			Update s 
			 SET 
            s.UsedQty = ISNULL(c.UsedQty,0) + @DistributedMT,
            s.CurrentStock = c.CurrentStock - @DistributedMT,
            s.UpdatedDate = GETDATE(),
			s.UpdatedBy = @CreatedBy
			from M_FertilizerStock s
			inner join ctestock c
			on s.StockId=c.StockId


   --     UPDATE M_FertilizerStock
   --     SET 
   --         UsedQty = ISNULL(UsedQty,0) + @DistributeQty,
   --         CurrentStock = CurrentStock - @DistributeQty,
   --         UpdatedDate = GETDATE(),
   --         UpdatedBy = @CreatedBy
			--WHERE FertilizerID = @FertilizerId
			--AND SocietyId = @SocietyId;
			

        --  Transaction Log
        INSERT INTO tbl_FertilizerStockTransactionsLog
        (
            FertilizerID,
            TransactionTypeStatusId,
            Quantity,
            TransactionDate,
            Remarks,
            IsActive,
            CreatedDate,
            CreatedBy,
            SocietyId,
            DemandDetailsId
        )
        VALUES
        (
            @FertilizerId,
            @StatusId,
            @DistributeQty,
            GETDATE(),
            ISNULL(@Remarks,'Distributed to Farmer'),
            1,
            GETDATE(),
            @CreatedBy,
            @SocietyId,
            @DemandDetailsId
        );

        COMMIT TRAN;
        --SELECT 'SUCCESS' AS Status;

    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAvlStockByFertilizer]    Script Date: 1/31/2026 8:15:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- sp_GetAvlStockByFertilizer  2,10014,1,1,2
-- sp_GetAvlStockByFertilizer 10014,1,1
CREATE   PROCEDURE [dbo].[sp_GetAvlStockByFertilizer]
   @Action int =0,
   @SocietyId int,
	@FarmerId INT =NULL,
	@FertilizerId INT=null
	
	
AS
BEGIN
	IF(ISNULL(@FarmerId,0)=0 AND @Action=0)
	BEGIN
	IF EXISTS(SELECT 1 FROM M_FertilizerStock 
				where (FertilizerId = @FertilizerId and SocietyId= @SocietyId and IsActive=1))
	BEGIN
    SELECT 
     top 1  CAST(ISNULL(CurrentStock,0) as varchar(10)) AS Value
    FROM M_FertilizerStock
    WHERE ( FertilizerId = @FertilizerId and SocietyId= @SocietyId and IsActive=1)
	ORDER BY StockID DESC
	END
	ELSE
	BEGIN
	SELECT 0 AS value
	END
	END
	IF(ISNULL(@FarmerId,0)>0 AND @Action=0)--get fertilzerNeed by Farmer
	BEGIN
		select CAST(Sum(fdd.FertilizerNeed) AS DECIMAL(8,2)) FertilizerNeed,
		CAST(SUM(ISNULL(RecieveQty,0)) AS decimal(8,2)) RecieveQty,
		Max(FDD.RequestId)DemandDetailsId
		from [dbo].[tbl_FertilizerDemend] FD INNER JOIN 
		[dbo].[tbl_FertilizerDemendDetails] FDD
		ON FD.Id=FDD.RequestId
		LEFT JOIN [dbo].[M_UserRoleMapping] rm
		on fd.samitiid=rm.Userid
		where fd.Farmerid=@FarmerId
		and status='Pending' 
		and fd.Isactive=1 and rm.UserNumber=@SocietyId
		and fertilizerId=@FertilizerId
		--GROUP BY FertilizerId,FDD.RequestId
	
	END

	if(@Action=1)-- get demand details
	begin 
	select
	Row_number() Over(order by fdd.Id asc) as '???????',
	CASE WHEN ISNULL(FertilizerNeed,0)>ISNULL(RecieveQty,0) THEN  
	 '<input type="button" class="btnView"
            data-id="' + CAST(fdd.Id AS VARCHAR(50)) + '"
            data-fertilizer="' + CAST(fdd.FertilizerId AS VARCHAR(50)) + '"
            data-farmer="' + fr.FarmerName + '"
            value="Distribute" />'
 ELSE '' END
	as Action,
	fr.FarmerName                AS N'????? ?? ???',
    fd.RequestNo          AS N'?????? ???????',
    --fd.FarmerId           AS N'????? ????',
    --fd.SamitiId           AS N'????? ????',
    CONVERT(VARCHAR, fd.CreatedDate, 0) AS N'???? ????',
    cr.CropNameHindi      AS N'??? ?? ???',
    f.FertilizerNameHindi AS N'?????? ?? ???',
    fdd.Amount            AS N'????',
    fdd.FertilizerNeed    AS N'?????? ??????',
   ISNULL(RecieveQty,0)        AS N'??????? ??????'
	from tbl_FertilizerDemend fd left join tbl_FertilizerDemendDetails fdd on fd.Id=fdd.RequestId
	left join M_UserRoleMapping rm on fd.SamitiId=rm.UserId
	left join tbl_FarmersRegistration fr on fd.FarmerId=fr.FarmerId
	left join M_Society s on fd.SamitiId=s.Id
	left join M_CropMaster cr on fdd.CropId=cr.CropId
	left join M_FertilizerMaster f on fdd.FertilizerId=f.FertilizerId
	where fd.SamitiId=rm.UserId and rm.UserNumber=@SocietyId
	and fd.FarmerId=@FarmerId and (fdd.FertilizerId=@FertilizerId or @FertilizerId is null)
	end
	if(@Action=2)
	BEGIN
	;WITH CTE AS (SELECT
	ROW_NUMBER()OVER(PARTITION BY FS.FertilizerID ORDER BY STOCKID DESC) RNK,
	FertilizerNameHindi,CurrentStock FROM M_FertilizerStock FS
	INNER JOIN M_FertilizerMaster FM
	ON FS.FertilizerID=FM.FertilizerId
	WHERE FS.SocietyId =@SocietyId
	)
	SELECT FertilizerNameHindi '?????? ?? ???',
	Cast((CurrentStock) as decimal(18,3)) as '??????? ?????? ????? (M.T)'
	FROM CTE WHERE RNK=1
	--AND  FS.FertilizerID=1
	
	END
END

GO

/****** Object:  StoredProcedure [dbo].[sp_SaveUpdateGetFertilizerStock]    Script Date: 1/31/2026 8:15:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--select * from [dbo].[M_FertilizerStock]

CREATE   PROCEDURE [dbo].[sp_SaveUpdateGetFertilizerStock]
(
	@CompanyId		Int,
	@SocietyId      INT ,
	@FertilizerID   INT,
	@StockID        INT = 0,
    @OpeningStock   DECIMAL(18,3) = NULL,
    @PurchasedQty   DECIMAL(18,3),
    @UsedQty        DECIMAL(18,3) = NULL,
    @QtyUnit        VARCHAR(20) = NULL,
    @UserId         INT,
    @Remarks        VARCHAR(255) = NULL
	
)
AS
BEGIN
    --SET NOCOUNT ON;
    BEGIN TRAN;

    BEGIN TRY

	
DECLARE @KgToMT DECIMAL(18,6) = 1000.0;
DECLARE 
    @OpeningStockMT DECIMAL(18,6),
    @PurchasedQtyMT DECIMAL(18,6),
    @UsedQtyMT      DECIMAL(18,6);

SET @OpeningStockMT = ISNULL(@OpeningStock,0) ;
SET @PurchasedQtyMT = ISNULL(@PurchasedQty,0) ;
SET @UsedQtyMT      = ISNULL(@UsedQty,0) ;




        DECLARE @CurrentStock DECIMAL(18,6);

		SET @CurrentStock =
      ISNULL(@OpeningStockMT,0)
    + ISNULL(@PurchasedQtyMT,0)
    - ISNULL(@UsedQtyMT,0);


        -- ================= INSERT / UPDATE STOCK =================
        IF  NOT EXISTS ( SELECT 1 FROM M_FertilizerStock WHERE FertilizerID= @FertilizerID  AND IsActive=1)
        BEGIN
            INSERT INTO dbo.M_FertilizerStock
            (
                FertilizerID,
                OpeningStock,
                PurchasedQty,
                UsedQty,
                CurrentStock,
                QtyUnit,
                SocietyId,
                IsActive,
                CreatedDate,
                CreatedBy,
				PurchaseByCompanyId
            )
            VALUES
            (
                @FertilizerID,
                @OpeningStock,
                @PurchasedQty,
                @UsedQty,
                @CurrentStock,
                @QtyUnit,
                @SocietyId,
                1,
                GETDATE(),
                @UserId,
				@CompanyId
            );

            SET @StockID = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            UPDATE dbo.M_FertilizerStock
            SET
               OpeningStock = ISNULL(OpeningStock, 0) + ISNULL(@OpeningStock, 0),
				PurchasedQty = ISNULL(PurchasedQty, 0) + ISNULL(@PurchasedQty, 0),
				UsedQty      = ISNULL(UsedQty, 0) + ISNULL(@UsedQty, 0),
                CurrentStock = @CurrentStock,
                QtyUnit      = @QtyUnit,
                SocietyId    = @SocietyId,
                UpdatedDate  = GETDATE(),
                UpdatedBy    = @UserId,
				PurchaseByCompanyId=@CompanyId
            WHERE FertilizerID= @FertilizerID
			and IsActive=1;
        END

        -- ================= INSERT TRANSACTION LOG =================

        
        -- Purchase Entry
        IF ISNULL(@PurchasedQty,0) > 0
        BEGIN
            INSERT INTO dbo.tbl_FertilizerStockTransactionsLog
            (
                FertilizerID,
                TransactionTypeStatusID,
                Quantity,
                TransactionDate,
                Remarks,
                IsActive,
                CreatedDate,
                CreatedBy,
                SocietyId
            )
            VALUES
            (
                @FertilizerID,
                2,
                @PurchasedQty,
                GETDATE(),
                'Purchase Entry',
                1,
                GETDATE(),
                @UserId,
                @SocietyId
            );
        END

        COMMIT TRAN;
        SELECT @StockID AS StockID;

    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

/****** Object:  StoredProcedure [dbo].[sp_getSocietyFertilizerDetails]    Script Date: 1/31/2026 8:15:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- sp_getSocietyFertilizerDetails '9918999991'
CREATE   PROC [dbo].[sp_getSocietyFertilizerDetails]
@MobileNo numeric 
as Begin

---==================================FOR DASHBOARD COUNT================================================================

declare @TotalfertilizerNeed decimal(10,2), @TotalfertilizeDistributed decimal(10,2),
 @TotalfertilizeStockBySociety varchar(20),@SocityId int=0

 set @SocityId =(select Id from M_Society where Mobile=@MobileNo and IsActive=1)

set @Totalfertilizerneed =(
							select Isnull(Sum(Fertilizerneed),0)Need from [dbo].[tbl_FertilizerDemend]
							left join tbl_FertilizerDemendDetails 
							on tbl_FertilizerDemend.Id=tbl_FertilizerDemendDetails.RequestId
							--Inner Join M_UserRoleMapping on [M_FertilizerStock].SocietyId=M_UserRoleMapping.UserNumber
							LEFT JOIN M_Society S on S.Id=tbl_FertilizerDemend.SamitiId
							where  tbl_FertilizerDemend.SamitiId=@SocityId
							--cast(tbl_FertilizerDemend.CreatedDate as Date)=Cast(GETDATE() as Date)
							AND s.Mobile=@MobileNo 

							)

set @TotalfertilizeDistributed=(select sum(fdd.recieveQty)
								from 
								tbl_FertilizerDemend fd inner join tbl_FertilizerDemendDetails fdd
								on fd.Id=fdd.RequestId
								Inner Join M_UserRoleMapping on fd.SamitiId =M_UserRoleMapping.UserId
								LEFT JOIN M_Society S on S.Id=M_UserRoleMapping.UserId
								where fd.SamitiId=@SocityId and  s.Mobile=@MobileNo
								--GROUP BY FDD.RequestId

								)

;with cteStock as(
select  Cast(isnull([CurrentStock],0) as decimal(8,2))CurrentStock,
FertilizerID,StockID,OpeningStock,
ROW_NUMBER() OVER (PARTITION BY FertilizerID ORDER BY stockId desc) rnk
from M_FertilizerStock
LEFT Join M_UserRoleMapping on [M_FertilizerStock].SocietyId=M_UserRoleMapping.UserNumber
LEFT JOIN M_Society S on S.Id=M_UserRoleMapping.UserId
where s.Id=@SocityId
)

SELECT @TotalfertilizeStockBySociety = (SUM(CurrentStock))-- MITRIC TON
FROM cteStock
WHERE rnk = 1;

;With Cte as 
(
select 
(select( Cast(@TotalfertilizerNeed as decimal(8,2))))TotalfertilizerNeed,
(select	 (CAST(@TotalfertilizeDistributed AS decimal(8,2))))TotalDistributed,
(select 
(
case when @Totalfertilizerneed>@TotalfertilizeDistributed
THEN CAST( @Totalfertilizerneed- @TotalfertilizeDistributed AS  decimal(8,2))
ELSE 0 END
)

) 

TotalfertilizerPending ,
(
select CAST(@TotalfertilizeStockBySociety AS DECIMAL(8,2) )
)CurrentStock 

)

select * from Cte

----------------------------------------END DASHBOARD COUNT--------------------------------------------------------------------
	
---------------------------------------Village wise Demand --------------------------------------------------------------------

select   v.VillageNameHi as ItemName,CAST(MAX(dt.FertilizerNeed) as int)DemandKg 
from [dbo].[tbl_FertilizerDemend] fd
inner join  tbl_FertilizerDemendDetails dt on fd.id=dt.RequestID
left join tbl_FarmerLandDetails l on dt.farmerId=l.farmerId
left join M_Village v
on l.VillageId=v.Id
where fd.samitiId=@SocityId
group by v.VillageNameHi,dt.FertilizerNeed

---=======================================--END VILLAGE WISE DEMAND--===========================================

---------------- FERTILIZER WISE DEMAND-------------------------------
  

select 
Max(dt.FertilizerNeed) DemandKg,f.FertilizerNameHindi as ItemName
from [dbo].[tbl_FertilizerDemend] fd
inner join  tbl_FertilizerDemendDetails dt on fd.id=dt.RequestID
left join tbl_FarmerLandDetails l on dt.farmerId=l.farmerId
LEFT JOIN M_FertilizerMaster F ON DT.FERTiLIZERID=f.fertilizerId
where fd.samitiId=@SocityId
group by RequestId,f.FertilizerNameHindi


-------------------------==============================END=======================------------------------------------------

End
GO


