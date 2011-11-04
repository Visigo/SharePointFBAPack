<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>會員審批通過</title>
      </head>
      <body>
        <div>
          <br />
          <p>
            你在 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 申請的賬號已獲審批通過，並且已授權訪問網站。
          </p>
          <p>
            你的用戶是： <xsl:value-of select="MembershipRequest:get_UserName()" />
          </p>
          <p>
            首次登入密碼是： <xsl:value-of select="MembershipRequest:get_Password()" />
          </p>
          <p></p>

          <p>
            <xsl:text>你的首次登入密碼可在登入後馬上通過此頁面</xsl:text>
            <a>
              <xsl:attribute name="href">
                <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
              </xsl:attribute>
              <xsl:text>，</xsl:text>
            </a>
            <xsl:text> 使用用戶名及首次登入密碼進行更改。</xsl:text>
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
          <p>
            <xsl:text>更改好密碼後，將可以瀏覽通過 </xsl:text>
            <xsl:value-of select="MembershipRequest:get_SiteURL()" />
            <xsl:text> 訪問網站 </xsl:text>
            <xsl:value-of select="MembershipRequest:get_SiteName()" />
          </p>

          <p>如你需要額外的訪問權限，請聯繫管理員。</p>

          <p>謝謝！</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>