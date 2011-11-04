<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>恢復密碼</title>
      </head>
      <body>
        <div>
          <p>
            你收到這個郵件是因為你忘記了網站 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 的密碼。
          </p>

          <p>
            你的用戶名： <xsl:value-of select="MembershipRequest:get_UserName()" />
          </p>

          <p>
            你的臨時密碼： <xsl:value-of select="MembershipRequest:get_Password()" />
          </p>

          <p></p>

          <p>
            <xsl:text>你的臨時密碼可在登入後馬上通過此頁面 </xsl:text>
            <a>
              <xsl:attribute name="href">
                <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
              </xsl:attribute>
              <xsl:text>，</xsl:text>
            </a>
            <xsl:text> 使用用戶名及臨時密碼進行更改。</xsl:text>
          </p>
          <p>
            <xsl:text>更改密碼的頁面： </xsl:text>
            <a>
              <xsl:attribute name="href">
                <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
              </xsl:attribute>
              <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
            </a>
            <xsl:text>。</xsl:text>

          </p>

          <p>謝謝！</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>