<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>恢复密码</title>
      </head>
      <body>
        <div>
            <p>你收到这个邮件是因为你忘记了网站 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 的密码。</p>

          <p>
            你的用户名： <xsl:value-of select="MembershipRequest:get_UserName()" />
          </p>

          <p>你的临时密码： <xsl:value-of select="MembershipRequest:get_Password()" /></p>

            <p></p>

            <p>
                <xsl:text>你的临时密码可在登入后马上通过此页面 </xsl:text>
                <a>
                    <xsl:attribute name="href">
                        <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
                    </xsl:attribute>
                    <xsl:text>，</xsl:text>
                </a>
                <xsl:text> 使用用户名及临时密码进行更改。</xsl:text>
            </p>
            <p>
              <xsl:text>更改密码的页面： </xsl:text>
              <a>
                <xsl:attribute name="href">
                  <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
                </xsl:attribute>
                <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
              </a>
              <xsl:text>。</xsl:text>

            </p>

            <p>谢谢！</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>