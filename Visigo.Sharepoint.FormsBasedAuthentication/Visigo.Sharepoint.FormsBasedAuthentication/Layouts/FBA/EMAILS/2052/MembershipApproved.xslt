<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>会员审批通过</title>
      </head>
      <body>
        <div>
            <br />
            <p>你在 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 申请的账号已获审批通过，并且已授权访问网站。</p>
            <p>你的用户是： <xsl:value-of select="MembershipRequest:get_UserName()" /></p>
            <p>首次登入密码是： <xsl:value-of select="MembershipRequest:get_Password()" /></p>
            <p></p>

            <p>
                <xsl:text>你的首次登入密码可在登入后马上通过此页面</xsl:text>
                <a>
                    <xsl:attribute name="href">
                        <xsl:value-of select="MembershipRequest:get_ChangePasswordURL()" />
                    </xsl:attribute>
                    <xsl:text>，</xsl:text>
                </a>
                <xsl:text> 使用用户名及首次登入密码进行更改。</xsl:text>
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
            <p>
                <xsl:text>更改好密码后，将可以浏览通过 </xsl:text>
                <xsl:value-of select="MembershipRequest:get_SiteURL()" />
                <xsl:text> 访问网站 </xsl:text>
                <xsl:value-of select="MembershipRequest:get_SiteName()" />
            </p>

            <p>如你需要额外的访问权限，请联系管理员。</p>

            <p>谢谢！</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>