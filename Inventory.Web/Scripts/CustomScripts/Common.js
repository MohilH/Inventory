
function ValidateForm(FormID) {
    debugger;
    var error_Count = 0;//This variable is used to scroll to first error element only once even if there are multiple errors on page
    $("#" + FormID).validate({
        showErrors: function (errorMap, errorList) {
 
          
            // Clean up any tooltips for valid elements
            $.each(this.validElements(), function (index, element) {
            
                $(element).data("title", "") // Clear the title - there is no error associated anymore
                    .removeClass("error")
                    .tooltip("destroy");
                error_Count = 0;
            });

            // Create new tooltips for invalid elements
            $.each(errorList, function (index, error) {
       
              
                $(error.element).tooltip("destroy") // Destroy any pre-existing tooltip so we can repopulate with new tooltip content
                    .data("title", error.message)
                    .addClass("error")
                    .tooltip(); // Create a new tooltip based on the error messsage we just set in the title 

                if (error_Count == 0) {
             
                    //SCROLL TO FIRST ERROR ELEMENT ON PAGE
                    $('html, body').animate({
                        scrollTop: $(".error:first").offset().top - 50
                    }, 500);
                }
                error_Count = error_Count + 1;
            });
        }
    });
}


///////----Common function for Pop Up---///////////(24052017)

function ShowPopupFrame(InnerPartialContent) {
    debugger;
    $('#DivCommonPoupDiv').fadeIn().addClass('pp');
    $('#DivPoupContantContainer').addClass('openbounse');
    $('#DivPoupContant').empty().append(InnerPartialContent);
}
function HidePopupFrame() {
    $('#DivPoupContantContainer').removeClass('openbounse');
    $('#DivCommonPoupDiv').fadeOut().removeClass('pp');
    $('#DivPoupContantContainer').removeClass('openbounse');
    $('#DivPoupContant').empty();
}