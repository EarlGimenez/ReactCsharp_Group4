# Contact Form Email Setup Guide

## ?? Overview

The contact form backend is now fully implemented with email functionality. Users can submit contact forms via the React frontend, and the messages will be sent to **earlreynan.gimenez.22@usjr.edu.ph** via email.

---

## ?? What Was Implemented

### **Backend Components Created:**

1. **EmailSettings.cs** - Email configuration model
2. **ContactFormViewModel.cs** - Contact form request/response models
3. **IEmailService.cs** - Email service interface
4. **EmailService.cs** - Email sending implementation using SMTP
5. **ContactController.cs** - API endpoint for contact form submissions

### **API Endpoint:**
```
POST https://localhost:59453/api/contact
```

### **Test Endpoint:**
```
GET https://localhost:59453/api/contact/test
```

---

## ?? Email Configuration Setup

### **Step 1: Get Gmail App Password**

Since you're using Gmail, you need to generate an **App Password**:

1. Go to your Google Account: https://myaccount.google.com/
2. Navigate to **Security**
3. Enable **2-Step Verification** (if not already enabled)
4. Go to **App passwords** (search for it in settings)
5. Select **Mail** as the app and **Other** as the device
6. Name it "BookIt SMTP"
7. Click **Generate**
8. **Copy the 16-character password** (you'll use this in appsettings.json)

### **Step 2: Update appsettings.json**

Open `ASI.Basecode.WebApp/appsettings.json` and update the email settings:

```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderEmail": "your-gmail-address@gmail.com",
  "SenderName": "BookIt Room Booking System",
  "RecipientEmail": "earlreynan.gimenez.22@usjr.edu.ph",
  "Username": "your-gmail-address@gmail.com",
  "Password": "xxxx xxxx xxxx xxxx",  // ? Paste your 16-char App Password here
  "EnableSsl": true
}
```

**Replace:**
- `your-gmail-address@gmail.com` with your actual Gmail address
- `xxxx xxxx xxxx xxxx` with the App Password you generated

---

## ?? Testing the Implementation

### **Option 1: Test via Browser/Postman**

Test the endpoint directly:

**URL:** `https://localhost:59453/api/contact/test`  
**Method:** GET  
**Expected Response:**
```json
{
  "success": true,
  "message": "Contact API is working!",
  "timestamp": "2025-01-15T10:30:00"
}
```

**Submit Contact Form:**

**URL:** `https://localhost:59453/api/contact`  
**Method:** POST  
**Headers:** `Content-Type: application/json`  
**Body:**
```json
{
  "name": "Test User",
  "email": "test@example.com",
  "subject": "Test Message",
  "message": "This is a test contact form submission."
}
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Thank you for contacting us! We will get back to you soon."
}
```

### **Option 2: Test via Frontend**

Your existing frontend service (`contactService.ts`) is already configured to use:
```typescript
const API_URL = 'https://localhost:59453/api/contact';
```

Just use your React contact form component and it should work!

---

## ?? Email Template

When a contact form is submitted, the recipient will receive a beautifully formatted HTML email:

**Email Structure:**
- **From:** BookIt Room Booking System
- **To:** earlreynan.gimenez.22@usjr.edu.ph
- **Subject:** Contact Form: [User's Subject]

**Email Body Includes:**
- Sender's name
- Sender's email (clickable mailto link)
- Subject
- Message content
- Timestamp of submission

---

## ?? Troubleshooting

### **Issue: SSL/TLS Errors**

If you get SSL errors, ensure:
1. You're using Gmail App Password (not regular password)
2. 2-Step Verification is enabled on your Google account
3. SMTP port is `587` (TLS)
4. `EnableSsl` is set to `true`

### **Issue: Authentication Failed**

- Double-check the App Password (no spaces)
- Ensure the Gmail account matches in both `SenderEmail` and `Username`
- Make sure the App Password hasn't been revoked

### **Issue: CORS Errors**

Already handled! Your CORS configuration allows any localhost origin:
```csharp
.SetIsOriginAllowed(origin => 
{
    if (string.IsNullOrEmpty(origin)) return false;
    var uri = new Uri(origin);
    return uri.Host == "localhost" || uri.Host == "127.0.0.1";
})
```

### **Issue: Email Not Arriving**

1. Check spam/junk folder
2. Verify `RecipientEmail` is correct
3. Check backend logs in Visual Studio Output window
4. Try sending a test email via Postman first

---

## ?? Frontend Integration

Your frontend service is already set up! Just make sure:

1. Backend is running on `https://localhost:59453`
2. Frontend can reach the backend (SSL certificate trusted)
3. Contact form component is using `sendContactMessage()` from `contactService.ts`

**Example Usage in React:**
```typescript
import { sendContactMessage } from './services/contactService';

const handleSubmit = async (formData) => {
  try {
    const response = await sendContactMessage({
      name: formData.name,
      email: formData.email,
      subject: formData.subject,
      message: formData.message
    });
    
    if (response.success) {
      alert(response.message); // Success!
    }
  } catch (error) {
    console.error('Error:', error);
    alert('Failed to send message. Please try again.');
  }
};
```

---

## ?? Security Notes

1. **Never commit** `appsettings.json` with real email credentials to Git
2. Consider using **environment variables** for production:
   - `SMTP_PASSWORD` environment variable
   - Azure Key Vault
   - AWS Secrets Manager
3. The App Password can be revoked anytime from your Google Account settings

---

## ?? Alternative SMTP Providers

If you prefer not to use Gmail, you can use:

### **SendGrid:**
```json
{
  "SmtpServer": "smtp.sendgrid.net",
  "SmtpPort": 587,
  "Username": "apikey",
  "Password": "your-sendgrid-api-key"
}
```

### **Office 365/Outlook:**
```json
{
  "SmtpServer": "smtp-mail.outlook.com",
  "SmtpPort": 587
}
```

### **Mailgun:**
```json
{
  "SmtpServer": "smtp.mailgun.org",
  "SmtpPort": 587
}
```

---

## ? Checklist

- [x] EmailService created
- [x] Contact API controller created
- [x] Services registered in Startup.DI.cs
- [x] Email settings added to appsettings.json
- [x] Frontend service already configured
- [ ] **Generate Gmail App Password**
- [ ] **Update appsettings.json with credentials**
- [ ] **Test the endpoint**
- [ ] **Verify email arrives at earlreynan.gimenez.22@usjr.edu.ph**

---

## ?? You're All Set!

Once you've configured the Gmail App Password and updated `appsettings.json`, the contact form will:

1. ? Accept submissions from your React frontend
2. ? Validate the form data
3. ? Send a beautiful HTML email to your USJR email
4. ? Return success/error responses
5. ? Log everything for debugging

**Need help?** Check the Visual Studio Output window for detailed logs when testing!
