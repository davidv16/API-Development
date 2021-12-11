import requests
import json

email_template = '''
    <h2>Thank you for trading @ Just Trade It!</h2>
    %s
    <p>We hope you enjoyed our lovely service and don\'t hesitate to contact us if there are any questions.</p>
'''

new_trade_email_template = '''
    <p>You have a new trade request!</p>
'''

update_trade_email_template = '''
    <h3>Receiving Items:</h3>
    <table>
        <thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Title</th><th>Description</th></tr></thead>
        <tbody>%s</tbody>
    </table>
    <h3>Offering Items:</h3>
    <table>
        <thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Title</th><th>Description</th></tr></thead>
        <tbody>%s</tbody>
    </table>
    <p>
        From: %s
        IssuedDate: %s
        ModifiedDate: %s
        Status: %s
    </p>
'''

def send_simple_message(to, subject, body):
    print(to, subject, body)

    return requests.post(
        "https://api.mailgun.net/v3/sandboxcf1a5ac6a0774e37887f841e0530ff46.mailgun.org/messages",
        auth=("api", "459eb7a0b2fd4b6b1bbeb871cc9fb317-2ac825a1-945f0d74"),
        data={"from": "Mailgun Sandbox <postmaster@sandboxcf1a5ac6a0774e37887f841e0530ff46.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})

def send_new_trade_email(ch, method, properties, data):
    parsed_msg = json.loads(data)

    representation = email_template % new_trade_email_template
    send_simple_message(parsed_msg['Email'], 'New trade request!', representation)


def send_update_trade_email(ch, method, properties, data):
    parsed_msg = json.loads(data)

    receiving_items = parsed_msg['ReceivingItems']
    receiving_items_html = ''.join([ '<tr><td>%s</td><td>%s</td></tr>' % (item['Title'], item['ShortDescription']) for item in receiving_items ])
    offering_items = parsed_msg['OfferingItems']
    offering_items_html = ''.join([ '<tr><td>%s</td><td>%s</td></tr>' % (item['Title'], item['ShortDescription']) for item in offering_items ])
    sender = parsed_msg['Sender']
    representation = email_template % (update_trade_email_template % (
        receiving_items_html,
        offering_items_html,
        '%s (%s)' % (sender['FullName'], sender['Email']),
        parsed_msg['IssuedDate'],
        parsed_msg['ModifiedDate'],
        parsed_msg['Status']
    ))
    send_simple_message(parsed_msg['Email'], 'Trade request update!', representation)
