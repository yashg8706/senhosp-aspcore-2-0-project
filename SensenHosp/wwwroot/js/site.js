// Write your JavaScript code.

window.onload = function () {
    let donationAmount = 10;
    const amountInput = document.getElementById('donationAmount');
    amountInput.onblur = function () {
        donationAmount = Number(amountInput.value);
        console.log(donationAmount);
    }
    paypal.Buttons({
        createOrder: function (data, actions) {
            return actions.order.create({
                Prefer: "return=representation",
                "Content-Type": "Application/json",
                intent: "CAPTURE",
                purchase_units: [{
                    amount: {
                        currency: "CAD",
                        value: donationAmount
                    }
                }],
                application_context: {
                    brand_name: "Sensen Humber Hospital",
                    shipping_preference: "NO_SHIPPING",
                    user_action: "PAY_NOW"
                }
            });
        },
        /*createOrder: function () {
            return fetch('/Donations/CreateOrder', {
                method: 'post',
                headers: {
                    'content-type': 'application/json'
                }
            }).then(function (res) {
                return res.json();
            }).then(function (data) {
                return data.orderID;
            });
        },*/
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                console.log('Transaction completed by ' + details.payer.name.given_name);
                console.log('transaction id: ' + details.id);
                return fetch('/donations/GetOrder', {
                    method: 'post',
                    headers: {
                        'content-type': 'application/json'
                    },
                    body: JSON.stringify({
                        orderID: data.orderID
                    })
                });
            });
        }
    }).render('#paypal-button-container');
}