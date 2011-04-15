<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>Membership Request Rejected</title>
      </head>
      <body>
        <div>
          <p>Your membership request on <xsl:value-of select="MembershipRequest:get_SiteName()" /> has been rejected.</p>

          <p>If you feel that this is an error please try again or contact the site administrator.</p>

          <p>Thank you.</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>