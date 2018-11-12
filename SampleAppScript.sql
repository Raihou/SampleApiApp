USE raitesting
GO;

CREATE TABLE UpcomingPayments
(
	PaymentId int identity(1,1) primary key,
	PaymentDescription nvarchar(255) not null,
	Amount money not null,
	CreateDate datetime2 not null default GETDATE(),
	DueDate datetime2 not null,
	IsPaid bit not null default 0
) 
GO

CREATE PROCEDURE dbo.uspAddNewUpcomingPayment
@pAmount money, @pDueDate datetime2, @pPaymentDescription nvarchar(255)
as
BEGIN
	INSERT INTO dbo.UpcomingPayments(Amount, PaymentDescription, DueDate)
	VALUES(@pAmount, @pPaymentDescription, @pDueDate) 
END
GO

CREATE PROCEDURE dbo.uspMarkAsPaid
@pPaymentId int
as
BEGIN
	UPDATE dbo.UpcomingPayments
	SET IsPaid = 1
	WHERE PaymentId = @pPaymentId
END
GO

Create or Alter PROCEDURE dbo.uspGetAllUpcomingPayments
as
BEGIN
	SELECT 
		PaymentId as Id,
		PaymentDescription as  [Description],
		Amount as Amount,
		CreateDate as CreateDate,
		DueDate as DueDate,
		IsPaid as IsPaid,
		CASE 
			WHEN GETDATE() > DueDate and IsPaid = 0 then cast(1 as bit)
			ELSE cast(0 as bit) END as IsOverDue
	FROM dbo.UpcomingPayments
	order by DueDate desc
END
GO
