"use strict";

var AppHandlerAddRemoveTag = function () {

// Handle tag options
const handlerOptionsInput = () => {
    const tagElementOptions = document.querySelectorAll('input[name="tag_ElementOptions"]');
    const txtAddInput = document.getElementById('txt_add_input');
    const imageAddInput = document.getElementById('image_add_input');
    const videoAddInput = document.getElementById('video_add_input');
    const multiAddInput = document.getElementById('multi_add_input');

    tagElementOptions.forEach(option => {
        option.addEventListener('change', e => {
            const value = e.target.value;

            switch (value) {
                case '2': {
                    txtAddInput.classList.add('d-none')
                    imageAddInput.classList.remove('d-none');
                    videoAddInput.classList.remove('d-none');
                    multiAddInput.classList.remove('d-none');
                    break;
                }                    
                case '3': {
                    txtAddInput.classList.remove('d-none')
                    imageAddInput.classList.add('d-none');
                    videoAddInput.classList.remove('d-none');
                    multiAddInput.classList.remove('d-none');
                    break;
                }
                case '4': {
                    txtAddInput.classList.remove('d-none')
                    imageAddInput.classList.remove('d-none');
                    videoAddInput.classList.add('d-none');
                    multiAddInput.classList.remove('d-none');
                    break;
                }
                case '5': {
                    imageAddInput.classList.remove('d-none');
                    videoAddInput.classList.remove('d-none');
                    txtAddInput.classList.remove('d-none')
                    multiAddInput.classList.add('d-none');
                    break;
                }
                default: {
                    txtAddInput.classList.add('d-none')
                    imageAddInput.classList.add('d-none');
                    videoAddInput.classList.add('d-none');
                    multiAddInput.classList.add('d-none');
                    break;
                }
            }
        });
    });
}
    // Submit form handler
    const handleSubmit = () => {
        // Define variables
        let validator;

        // Get elements
        const form = document.getElementById('add_event_handler_form');
        // Handle submit button
        submitButton.addEventListener('click', e => {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable submit button whilst loading
                        submitButton.disabled = true;

                        setTimeout(function () {
                            submitButton.removeAttribute('data-kt-indicator');

                            Swal.fire({
                                text: "Form has been successfully submitted!",
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (result) {
                                if (result.isConfirmed) {
                                    // Enable submit button after loading
                                    submitButton.disabled = false;

                                    // Redirect to customers list page
                                    window.location = form.getAttribute("data-kt-redirect");
                                }
                            });
                        }, 2000);
                    } else {
                        Swal.fire({
                            html: "Sorry, looks like there are some errors detected, please try again. <br/><br/>Please note that there may be errors in the <strong>General</strong> or <strong>Advanced</strong> tabs",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                });
            }
        })
    }


    // Public methods
    return {
        init: function () {
            // Handle forms
            handlerOptionsInput();
            handleSubmit();
        }
    };
}();

// On document ready
APPHandlerUtil.onDOMContentLoaded(function () {
    AppHandlerAddRemoveTag.init();
});
