<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>Membership Password Recovery</title>
      </head>
      <body>
        <div>
            <p>You have requested this mail because you have forgotten your password to <xsl:value-of select="MembershipRequest:get_SiteName()" />.</p>

            <p>Your temporary password is: <xsl:value-of select="MembershipRequest:get_Password()" /></p>

            <p></p>

            <p>
                <xsl:text>Your temporary password can be changed immediately by logging onto the </xsl:text>
                <a>
                    <xsl:attribute name="href">
                        <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
                    </xsl:attribute>
                    <xsl:text>Change Password Page</xsl:text>
                </a>
                <xsl:text> using your user name and temporary password.</xsl:text>
            </p>
            <p>
                <xsl:text>The ChangePassword page is located at </xsl:text>
                <a>
                    <xsl:attribute name="href">
                        <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
                    </xsl:attribute>
                    <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
                </a>
                <xsl:text>.</xsl:text>

            </p>

            <p>Thank you.</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>