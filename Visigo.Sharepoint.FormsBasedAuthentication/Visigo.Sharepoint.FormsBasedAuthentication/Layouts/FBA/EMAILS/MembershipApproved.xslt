<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>Membership Approved</title>
      </head>
      <body>
        <div>
            <br />
            <p>Your request for an account on <xsl:value-of select="MembershipRequest:get_SiteName()" /> has been approved and you have been granted access.</p>
            <p>Your user name is: <xsl:value-of select="MembershipRequest:get_UserName()" /></p>
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
            <p>
                <xsl:text>Once you have changed your password, you can browse the </xsl:text>
                <xsl:value-of select="MembershipRequest:get_SiteName()" />
                <xsl:text> site at </xsl:text>
                <xsl:value-of select="MembershipRequest:get_SiteURL()" />
            </p>

            <p>If you need additional access please contact the site administrator.</p>

            <p>Thank you.</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>