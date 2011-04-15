<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>Membership Pending</title>
      </head>
      <body>
        <div>
          <br />
          <p>Thank you for requesting a Membership on <xsl:value-of select="MembershipRequest:get_SiteName()" />.</p>

          <p>You will receive an email with a temporary password once your request has been approved.</p>

          <p>If you have any additional questions or require assistance, please contact the site administrator.</p>

        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>