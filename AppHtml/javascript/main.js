function refreshPaymentList(){
  let ul = $('#paymentsList');
  $.ajax({
    url:'http://localhost:62551/api/payment/GetAllUpcomingPayments',
    type:'GET',
    success:function(response){
      ul.empty();
      $.each(response, function(index,element){
        let classColor = 'bg-primary';
        let status = 'Upcoming';
        let hasButton = '<button class="btn btn-light float-right markAsPaid">Pay</button>';
        if(element.isOverDue){
          classColor = 'bg-danger';
          status = 'Overdue!';
        }
        if(element.isPaid){
          classColor = 'bg-success';
          status = 'Paid';
          hasButton = '';
        }
        ul.append('<li class="list-group-item mb-2 text-white '+classColor+'" data-id="'+element.id+'">'+element.id+': '+element.description+' '+element.dueDate+' '+element.amount+' Status: '+status+' '+hasButton+'</li>')
      });
    },
    error:function(response){
      console.log(response);
    }
  })
}

function pay(button){
  var elem = $(button).closest('li');
  var id = elem.data('id');
  $.post('http://localhost:62551/api/payment/markaspaid/'+id,function(){
    refreshPaymentList();
  });
};

function addNewPayment(){
  var loginForm = $('#newPaymentForm').serializeArray();
  var loginFormObject = {};
  $.each(loginForm,
    function(i, v) {
        loginFormObject[v.name] = v.value;
    });

  var data =  JSON.stringify(loginFormObject);
  console.log(data);
  $.ajax({
      url:'http://localhost:62551/api/payment/AddUpcomingPayment',
      data:JSON.stringify(loginFormObject),
      type:'POST',
      contentType:  "application/json",
      success:function(response){
        $('#errorsDiv').hide();
        refreshPaymentList();
      },
      error:function(response){
        $('#errorsDiv').show();
        $('#errorsDiv').text(response.responseText);
      }
  })
};
